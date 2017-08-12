(function() {

    angular.module('app').controller('ReqController',
    ['$q', '$scope', '$rootScope', '$timeout', 'dataservice', 'logger', controller]);

    function controller($q, $scope, $rootScope, $timeout, dataservice, logger) {
        // The controller's API to which the view binds
        var vm = this;
        vm.addItem = addItem;
        vm.removeItem = removeItem;
        vm.items = [];
        vm.requests = [];
        vm.stores = [];
        $scope.requestSearch = "";
        vm.reqSearchFilter = reqSearchFilter;
        vm.requestsFulfilled = [];
        vm.requestAdded = requestAdded;
        vm.clearSelection = clearSelection;
        vm.maxHeight = '300px';
        vm.purchaseSelection = purchaseSelection;
        vm.itemColor = itemColor;
        vm.itemTextColor = itemTextColor;
        vm.checkNoSelection = checkNoSelection;

        if ($rootScope.userId == -1) {
            location.href = '#';
        }

        vm.loading_screen = window.pleaseWait({
            logo: "Content/AnkhsShop.png",
            backgroundColor: '#000000',
            loadingHtml: "<h1 class='loading-message'>Loading...</h1><div class='spinner'><div class='cube1'></div><div class='cube2'></div></div>"
        });

        var a = { Items: [], StoreId: -1, StoreLoc: '', StoreName: 'All Stores' };
        vm.stores[0] = a;

        var suspendSave = false;
        var today = new Date();

        getMaxHeight();

        // Start getting all the requests as soon as this controller is created
        getRequests();
        getStores();
        

        Date.prototype.addDays = function(days) {
            var dat = new Date(this.valueOf());
            dat.setDate(dat.getDate() + days);
            return dat;
        }

        function reqSearchFilter(request) {
            if (typeof request != "undefined" && $scope.selectedStore != "undefined") {
                //logger.info(request.Item.ItemName + " contains " + $scope.requestSearch.toLowerCase() + "? " + request.Item.ItemName.includes($scope.requestSearch.toLowerCase()));
                if (request.Bought == 1) {
                    if (request.Frequency == 0) {
                        return false;
                    } else {
                        var dateBought = new Date(request.DateBought).addDays((request.Frequency - 1));
                        dateBought.setHours(0);
                        if ((dateBought) > today) {
                            return false;
                        }
                    }
                }
                // Make sure item matches serch criteria
                if (request.Item.ItemName.toLowerCase().includes($scope.requestSearch.toLowerCase())) {
                    // Is the dropdown menu allowing all stores?
                    if ($scope.selectedStore.StoreId != -1) {
                        // Check if item is in the same store as the selected dropdown store
                        return request.Item.StoreId == $scope.selectedStore.StoreId;
                    } else {
                        return true
                    }
                } else {
                    return false;
                }
            }
        }

        function checkNoSelection(redirect) {
            if (vm.requestsFulfilled.length == 0) {
                if (redirect) {
                    location.href = '#newreq';
                } else {
                    return true;
                }
            } else {
                return false;
            }
        }

        function itemColor(requestId) {
            if (requestAdded(requestId)) {
                return 'green';
            } else {
                return 'white';
            }
        }

        function itemTextColor(requestId) {
            if (requestAdded(requestId)) {
                return 'white';
            } else {
                return 'black';
            }
        }

        function requestAdded(requestId) {
            return (vm.requestsFulfilled.indexOf(requestId) != -1);
        }

        function clearSelection() {
            vm.requestsFulfilled = [];
        }

        function getMaxHeight() {
            var windowHeight = window.innerHeight;
            //logger.log(windowHeight);
            if (window.innerWidth >= 700) {
                vm.maxHeight = windowHeight - (windowHeight * 0.35) + 'px';
            } else {
                vm.maxHeight = windowHeight - 199 + 'px';
            }
        }

        function purchaseSelection() {
            suspendSave = true;
            vm.requests.forEach(function (request) {
                // only set completed for selected requests
                if (vm.requestsFulfilled.indexOf(request.RequestId) != -1) {
                    request.Bought = true;
                    request.DateBought = new Date();
                }
            });
            suspendSave = false;
            save(true);
            clearSelection();
        }

        function addItem(requestId) {
            vm.requestsFulfilled.push(requestId);
        };

        function removeItem(requestId) {
            var index = vm.requestsFulfilled.indexOf(requestId);
            if (index > -1) {
                vm.requestsFulfilled.splice(index, 1);
            }
        }

        function getRequests() {
            //logger.info("Fetching Requests");
            dataservice.getRequests().then(querySucceeded);

            function querySucceeded(data) {
                vm.requests = data.results;
                vm.loading_screen.finish();
                //logger.info("Fetched Requests");
            }
        };

        function getStores() {
            dataservice.getStores().then(querySucceeded);

            function querySucceeded(data) {
                var index = 0;
                for (key in data.results) {
                    index++;
                    vm.stores[index] = data.results[key];
                }
                //logger.info("Fetched Stores");
            }
        };

        

        function save(force) {
            // Save if have changes to save AND
            // if must save OR (save not suspended AND not editing a Todo)
            if (dataservice.hasChanges() && 
                (force || (!suspendSave))) {
                    return dataservice.saveChanges();
            }
            // Decided not to save; return resolved promise w/ no result
            return $q.when(false); 
        }

    }
})();