(function (app) {
    app.controller('productEditController', productEditController);


    productEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function productEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.product = {
            UpdatedDate: new Date()
        }

        $scope.moreImages = [];

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '300px'
        }

        $scope.getSeoTitle = getSeoTitle;
        $scope.getProductCategories = getProductCategories;
        $scope.updateProduct = updateProduct;

        $scope.chooseImage = chooseImage;

        $scope.chooseMoreImages = chooseMoreImages;

        function chooseMoreImages() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                });
            }

            finder.popup();
        }

        function loadProductDetail() {
            debugger;
            apiService.get('/api/product/getbyid/' + $stateParams.id,
                null,
                function (rs) {
                    $scope.product = rs.data;
                    $scope.moreImages = JSON.parse(rs.data.MoreImages);
                },
                function (error) {
                    notificationService.displayError(error);
                });
        }

        function chooseImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                });

            }
            finder.popup();
        }



        function updateProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
            apiService.put('/api/product/update', $scope.product,
                function (rs) {
                    notificationService.displaySuccess(rs.data.Name + ' đã được cập nhật !');
                    $state.go('products');
                },
                function () {
                    notificationService.displayError('Cập nhật không thành công !');
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
                    console.log('Cannot get list product catgory !');
                }
            );
        }

        getSeoTitle();
        getProductCategories();
        loadProductDetail();

    }
})(angular.module('onlineShop.products'));