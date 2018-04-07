(function (app) {
    app.controller('productListController', productListController);

    productListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function productListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.products= [];
        $scope.keyword = '';
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.totalCount = 0;
        $scope.isAll = false;


        $scope.getProducts = getProducts;
        $scope.selectAll = selectAll;
        $scope.search = search;
        $scope.deleteObject = deleteObject;
        $scope.deleteMulti = deleteMulti;

        $scope.$watch('products',
            function (n, o) {
                var checked = $filter('filter')(n,
                    {
                        checked: true
                    }
                );
                if (checked.length) {
                    $scope.selected = checked;
                    $('#btn-delete-all').removeAttr('disabled');
                } else {
                    $('#btn-delete-all').attr('disabled', 'disabled');
                }
            }, true);

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

            apiService.del('/api/product/deletemulti',
                config,
                function(rs) {
                    notificationService.displaySuccess('Xóa thành công ' + $scope.selected.length + ' bản ghi !');
                    search();
                },
                function() {
                    notificationService.displayError('Xóa không thành công !');
                });

        }

        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.products,
                    function (item) {
                        item.checked = true;
                    });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.products,
                    function (item) {
                        item.checked = false;
                    });
                $scope.isAll = false;
            }
        }

        function deleteObject(id) {
            $ngBootbox.confirm('Bạn có muốn xóa bản ghi này ?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }

                apiService.del('/api/product/delete',
                    config,
                    function () {
                        notificationService.displaySuccess('Xóa thành công !');

                    },
                    function () {
                        notificationService.displayError('Xóa không thành công !');
                    });
            });
        }

        function getProducts(page) {
           
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20

                }
            }

            apiService.get('/api/product/getall',
                config,
                function (rs) {
                    debugger;
                    if (rs.data.TotalCount === 0) {
                        notificationService.displayWarning('Không có bản ghi nào được tìm thấy !');
                    }

                    $scope.products = rs.data.Items;
                    $scope.page = rs.data.Page;
                    $scope.pagesCount = rs.data.TotalPages;
                    $scope.totalCount = rs.data.TotalCount;
                },
                function () {
                    console.log('Load product failed');
                });
        }

        function search() {
            getProducts();
        }

        $scope.getProducts();

    }
})(angular.module('onlineShop.products'));