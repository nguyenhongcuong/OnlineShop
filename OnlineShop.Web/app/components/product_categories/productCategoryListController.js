(function (app) {
    app.controller('productCategoryListController', productCategoryListController);

    productCategoryListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function productCategoryListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.productCategories = [];


        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';

        $scope.getProductCategories = getProductCategories;

        $scope.search = search;

        $scope.deleteObject = deleteObject;

        $scope.selectAll = selectAll;

        $scope.isAll = false;

        $scope.deleteMulti = deleteMulti;

        function deleteMulti() {
            var listId = [];
            $.each($scope.selected,
                function (i, item) {
                    listId.push(item.Id);
                });
            var config = {
                params: {
                    data: JSON.stringify(listId)
                }
            }
            apiService.del('/api/productcategory/deletemulti',
                config,
                function () {
                    notificationService.displaySuccess('Xóa thành công ' + $scope.selected.length + ' bản ghi !');
                    search();
                },
                function () {
                    notificationService.displayError('Xóa không thành công !');
                });
        }

        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.productCategories,
                    function (item) {
                        item.checked = true;
                    });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.productCategories,
                    function (item) {
                        item.checked = false;
                    });
                $scope.isAll = false;
            }
        }

        $scope.$watch("productCategories",
                function (n, o) {
                    debugger;
                    var checked = $filter('filter')(n, { checked: true });
                    if (checked.length) {
                        $scope.selected = checked;
                        $('#btn-delete-all').removeAttr('disabled');
                    } else {
                        $('#btn-delete-all').attr('disabled', 'disabled');
                    }
                }, true);

        function deleteObject(id) {
            $ngBootbox.confirm('Bạn có muốn xóa bản ghi này ?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('/api/productcategory/delete',
                    config,
                    function (rs) {
                        notificationService.displaySuccess('Xóa thành công !');
                        search();
                    });
            },
                function () {
                    notificationService.displayError('Xóa không thành công !');
                });
        }

        function search() {
            getProductCategories();
        }

        function getProductCategories(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            }


            apiService.get('/api/productcategory/getall', config,
                function (result) {
                    if (result.data.TotalCount === 0) {
                        notificationService.displayWarning('Không có bản ghi nào được tìm thấy !');
                    } else {
                        notificationService.displaySuccess('Đã tìm thấy ' + result.data.TotalCount);
                    }
                    $scope.productCategories = result.data.Items;
                    $scope.page = result.data.Page;
                    $scope.pagesCount = result.data.TotalPages;
                    $scope.totalCount = result.data.TotalCount;
                },
                function () {
                    console.log('Load product category failed');
                });
        }

        $scope.getProductCategories();

    }
})(angular.module('onlineShop.product_categories'));