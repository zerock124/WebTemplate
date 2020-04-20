define(["require", "exports", "../Share/PublicFunction"], function (require, exports, PublicFunction_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    var HomeService = (function () {
        function HomeService() {
        }
        HomeService.prototype.GetHomeDate = function () {
            var setting = {
                url: '/Home/GetHomeDate',
                type: 'GET',
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        return HomeService;
    }());
    var home_service = new HomeService();
    exports.default = home_service;
});
//# sourceMappingURL=service.js.map