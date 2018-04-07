(function (app) {
    app.controller('productAddController', productAddController);

    productAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function productAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.product = {
            CreatedDate: new Date(),
            Status: true,
            Name: 'Sản phẩm 1'
        }

        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px'
        }

        $scope.getSeoTitle = getSeoTitle;
        $scope.getProductCategories = getProductCategories;
        $scope.addProduct = addProduct;

        function addProduct() {
            apiService.post('/api/product/create',
                $scope.product,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm mới !');
                    $state.go('products');
                },
                function (error) {
                    notificationService.displayError('Thêm mới không thành công !');
                });
        }


        function getSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }

        function getProductCategories() {
            apiService.get('/api/productcategory/getallparents',
                null,
                function (rs) {
                    $scope.productCategories = rs.data;
                },
                function () {
                    console.log('Cannot get list oproduct category !');
                });
        }

        getSeoTitle();
        getProductCategories();
    }
})(angular.module('onlineShop.products'));