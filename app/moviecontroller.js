(function () {

    angular.module('app').controller('MovieController',
    ['$q', '$scope', '$rootScope', '$timeout', 'dataservice', 'logger', controller]);

    function controller($q, $scope, $rootScope, $timeout, dataservice, logger) {
        // The controller's API to which the view binds
        var vm = this;
        vm.movies = [];
        $scope.movieSearch = "";
        vm.movieSearchFilter = movieSearchFilter;
        vm.movieColor = movieColor;
        vm.movieTextColor = movieTextColor;
        vm.sequelNum = sequelNum;
        vm.maxHeight = '300px';
        vm.bluRayOptions = [true, false];
        vm.addMovie = addMovie;
        vm.inputError = false;
        vm.showSecTitle = showSecTitle;

        getMaxHeight();
        getMovies();

        function movieSearchFilter(movie) {
            if (typeof movie != "undefined") {
                var movieTitle = movie.MainTitle + ": " + movie.SecondaryTitle;
                // Make sure item matches search criteria
                if (movieTitle.toLowerCase().includes($scope.movieSearch.toLowerCase())) {
                    return true
                } else {
                    return false;
                }
            }
        }

        function sequelNum(sequelNum) {
            if (sequelNum == 1) {
                return "";
            } else {
                return " (" + sequelNum + ")"
            }
        }

        function movieColor(bluray) {
            if (bluray) {
                return 'deepskyblue';
            } else {
                return 'lightgray';
            }
        }

        function movieTextColor(bluray) {
            if (bluray) {
                return 'black';
            } else {
                return 'black';
            }
        }

        function showSecTitle(title) {
            if (isNaN(title)) {
                return ": " + title;
            } else {
                return "";
            }
        }

        function getMaxHeight() {
            var windowHeight = window.innerHeight;
            //logger.log(windowHeight);
            if (window.innerWidth >= 700) {
                vm.maxHeight = windowHeight - (windowHeight * 0.35) + 'px';
            } else {
                vm.maxHeight = windowHeight - 130 + 'px';
            }
        }

        function getMovies() {
            //logger.info("Fetching Movies");
            dataservice.getMovies().then(querySucceeded);

            function querySucceeded(data) {
                vm.movies = data.results;
                //vm.loading_screen.finish();
                //logger.info("Fetched Movies");
            }
        };

        function addMovie() {
            if (isNaN(vm.newMovieSequelNum) || vm.newMovieSequelNum == 0) {
                // Sequel Num entered incorrectly
                vm.inputError = true;
            } else if (vm.newMovieTitle == undefined) {
                // Main Title Missing
                vm.inputError = true;
            } else {
                // Attempt Database entry
                // Close Modal
                vm.inputError = false;
                $('#addMovie').modal('toggle');
            }
            logger.log("------------------------------");
            logger.log("Name: " + vm.newMovieTitle + ": " + vm.newMovieSecTitle);
            logger.log("SequelNum: " + vm.newMovieSequelNum);
            logger.log("BluRay: " + vm.selectedBluRayOption);
            logger.log("Valid: " + !vm.inputError);
            vm.newMovieTitle = undefined;
            vm.newMovieSecTitle = undefined;
            vm.newMovieSequelNum = undefined;
            vm.selectedBluRayOption = false;
        }
    }
})();