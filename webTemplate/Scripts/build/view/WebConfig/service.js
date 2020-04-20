define(["require", "exports", "../Share/PublicFunction"], function (require, exports, PublicFunction_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    var WebConfigService = (function () {
        function WebConfigService() {
        }
        WebConfigService.prototype.GetWebConfig = function (Id) {
            var setting = {
                url: "/WebConfig/GetWebConfig?Id=" + Id,
                type: 'GET',
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        WebConfigService.prototype.EditWebConfig = function (model) {
            var setting = {
                url: "/WebConfig/EditWebConfig",
                type: 'POST',
                data: model
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        return WebConfigService;
    }());
    var webconfig_service = new WebConfigService();
    exports.default = webconfig_service;
});
//# sourceMappingURL=service.js.map