var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
define(["require", "exports", "vue-property-decorator", "./ServiceItemList"], function (require, exports, vue_property_decorator_1, ServiceItemList_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    ServiceItemList_1 = __importDefault(ServiceItemList_1);
    var ServiceItemManagement = (function (_super) {
        __extends(ServiceItemManagement, _super);
        function ServiceItemManagement() {
            return _super !== null && _super.apply(this, arguments) || this;
        }
        ServiceItemManagement = __decorate([
            vue_property_decorator_1.Component({
                template: '#ServiceItemManagement',
                components: {
                    'service-item-list': ServiceItemList_1.default
                }
            })
        ], ServiceItemManagement);
        return ServiceItemManagement;
    }(vue_property_decorator_1.Vue));
    exports.default = ServiceItemManagement;
});
//# sourceMappingURL=ServiceItemManagement.js.map