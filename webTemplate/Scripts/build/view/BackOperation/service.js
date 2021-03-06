var __assign = (this && this.__assign) || function () {
    __assign = Object.assign || function(t) {
        for (var s, i = 1, n = arguments.length; i < n; i++) {
            s = arguments[i];
            for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p))
                t[p] = s[p];
        }
        return t;
    };
    return __assign.apply(this, arguments);
};
define(["require", "exports", "../Share/PublicFunction"], function (require, exports, PublicFunction_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    var BackOperationService = (function () {
        function BackOperationService() {
        }
        BackOperationService.prototype.GetBackOperationList = function (SearchModel, sendPagination) {
            var setting = {
                url: '/BackOperation/GetBackOperationList',
                type: 'POST',
                data: __assign(__assign({}, SearchModel), sendPagination)
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        return BackOperationService;
    }());
    var backOperation_service = new BackOperationService();
    exports.default = backOperation_service;
});
//# sourceMappingURL=service.js.map