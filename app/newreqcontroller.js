(function() {

    angular.module('app').controller('NewReqController',
    ['$q', '$scope', '$rootScope', '$timeout', 'dataservice', 'logger', controller]);

    function controller($q, $scope,  $rootScope, $timeout, dataservice, logger) {
        // The controller's API to which the view binds
        var vm = this;

        vm.items = [];
        vm.itemtypes = [];
        vm.units = [];
        vm.stores = [];
        $scope.itemSearch = "";
        vm.itemSearchFilter = itemSearchFilter;
        vm.requestItem = requestItem;
        vm.maxHeight = '300px';
        vm.selectedUnit = false;
        vm.addItem = addItem;

        if ($rootScope.userId == -1) {
            location.href = '#';
        }

        getMaxHeight();
        getItems();
        getItemTypes();
        getUnits();
        getStores();

        var a = { ItemTypeId: -1, ItemTypeName: 'Any Type', Items: [] };
        vm.itemtypes[0] = a;
        var b = { Items: [], StoreId: -1, StoreLoc: '', StoreName: 'All Stores' };
        vm.stores[0] = b;

        function getMaxHeight() {
            var windowHeight = window.innerHeight;
            //logger.log(windowHeight);
            if (window.innerWidth >= 700) {
                vm.maxHeight = windowHeight - (windowHeight * 0.35) + 'px';
            } else {
                vm.maxHeight = windowHeight - 162 + 'px';
            }
        }

        function itemSearchFilter(item) {
            if (typeof item != "undefined" && $scope.selectedItemType != "undefined") {
                if (item.ItemName.toLowerCase().includes($scope.itemSearch.toLowerCase())) {
                    if ($scope.selectedItemType.ItemTypeId != -1) {
                        return item.ItemType.ItemTypeId == $scope.selectedItemType.ItemTypeId;
                    } else {
                        return true
                    }
                } else {
                    return false;
                }
            }
        }

        function requestItem() {
            var unitId = 2;
            if (vm.selectedUnit == true) {
                unitId = 1;
            }
            if (vm.frequency == -1) {
                vm.frequency = 0;
            }

            var request = dataservice.createRequest({
                Amount: vm.amount,
                Frequency: vm.frequency,
                DateAdded: new Date(),
                Bought: false,
                UnitId: unitId,
                ItemId: vm.selectedItem.ItemId,
                UserId: $rootScope.userId
            });

            save(true).catch(addFailed);

            vm.selectedUnit = false;
            vm.amount = undefined;
            vm.frequency = undefined;

            function addFailed() {
                logger.log("Error Creating Request");
            }
        }

        function addItem() {
            var item = dataservice.createItem({
                ItemName: vm.newItemName,
                AisleNumber: vm.newItemAisle,
                StoreId: vm.newSelectedStore.StoreId,
                ItemTypeId: vm.newSelectedItemType.ItemTypeId
            });

            save(true).then(getItems).catch(addFailed);

            vm.newItemName = undefined;
            vm.newItemAisle = undefined;
            vm.newSelectedItemType = vm.itemtypes[0];
            vm.newSelectedStore = vm.stores[0];

            function addFailed() {
                logger.log("Error Creating Item");
            }
        }

        function getItems() {
            //logger.info("Fetching Items");
            dataservice.getItems().then(querySucceeded);

            function querySucceeded(data) {
                vm.items = data.results;
                //logger.info("Fetched Items");
            }
        };

        function getItemTypes() {
            //logger.info("Fetching Items");
            dataservice.getItemTypes().then(querySucceeded);

            function querySucceeded(data) {
                var index = 0;
                for (key in data.results) {
                    index++;
                    vm.itemtypes[index] = data.results[key];
                }
                //vm.itemtypes = data.results;
                //logger.info("Fetched Items");
            }
        };

        function getUnits() {
            //logger.info("Fetching Items");
            dataservice.getUnits().then(querySucceeded);

            function querySucceeded(data) {
                vm.units = data.results;
                //logger.info("Fetched Items");
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