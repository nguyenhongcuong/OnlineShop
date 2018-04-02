(function (app) {
    app.controller('productCategoryEditController', productCategoryEditController);


    productCategoryEditController.$inject = [
        'apiService', '$scope', 'notificationService', '$state',
        '$stateParams', 'commonService'
    ];


    function productCategoryEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {

        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true
        }

        $scope.updateProductCategory = updateProductCategory;

        $scope.getSeoTitle = getSeoTitle;

        function getSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }

        function loadProductCategoryDetail() {
            debugger;
            apiService.get('/api/productCategory/getbyid/' + $stateParams.id,
                null,
                function (result) {
                    $scope.productCategory = result.data;
                },
                function (error) {
                    notificationService.displayError(error);
                });
        }

        function updateProductCategory() {
            debugger;
            apiService.put('/api/productcategory/update',
                $scope.productCategory,
                function(result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật !');
                    $state.go('product_categories');
                },
                function() {
                    notificationService.displayError('Cập nhật không thành công !');
                });
        }

        function loadParentCategory() {
            apiService.get('/api/productcategory/getallparents',
                null,
                function(result) {
                    $scope.parentCategories = result.data;
                },
                function() {
                    console.log('Cannot get list parent !');
                });
        }

        loadParentCategory();
        loadProductCategoryDetail();
    }


})(angular.module('onlineShop.product_categories'));