define(["require", "exports", "../Share/PublicFunction"], function (require, exports, PublicFunction_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    var ServiceItemService = (function () {
        function ServiceItemService() {
        }
        ServiceItemService.prototype.GetServiceItemList = function () {
            var setting = {
                url: '/ServiceItem/GetServiceItemList',
                type: 'GET'
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        ServiceItemService.prototype.CreateServiceItemList = function (model) {
            var setting = {
                url: '/ServiceItem/CreateServiceItemList',
                type: 'POST',
                data: model
            };
            return PublicFunction_1.AjaxReturn(setting, 'FormData');
        };
        ServiceItemService.prototype.DeleteServiceItem = function (model) {
            var setting = {
                url: '/ServiceItem/DeleteServiceItem',
                type: 'POST',
                data: model
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        return ServiceItemService;
    }());
    var fonthome_service = new ServiceItemService();
    exports.default = fonthome_service;
});
//# sourceMappingURL=service.js.map