(function () {

    angular.module('app').controller('LoginController',
    ['$q', '$scope', '$rootScope', '$timeout', 'dataservice', 'logger', controller]);

    function controller($q, $scope, $rootScope, $timeout, dataservice, logger) {
        // The controller's API to which the view binds
        var vm = this;

        vm.verifyPassword = verifyPassword;
        vm.showFailure = false;
        vm.attempts = 0;
        $rootScope.userId = 1; //TODO: -1 for logged out

        function verifyPassword() {
            dataservice.verifyPass(vm.inputUserName, vm.inputPassword).then(querySucceeded);

            function querySucceeded(data) {
                if (data[0] = "true") {
                    $rootScope.userId = data[1];
                    location.href = '#view';
                } else {
                    vm.attempts += 1;
                    $scope.$apply();
                }
            }
        }
    }
})();