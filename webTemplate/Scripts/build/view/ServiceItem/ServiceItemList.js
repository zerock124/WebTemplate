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
define(["require", "exports", "vue-property-decorator", "./service", "./ServiceItemListItem", "moment"], function (require, exports, vue_property_decorator_1, service_1, ServiceItemListItem_1, moment) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    service_1 = __importDefault(service_1);
    ServiceItemListItem_1 = __importDefault(ServiceItemListItem_1);
    var ServiceItemList = (function (_super) {
        __extends(ServiceItemList, _super);
        function ServiceItemList() {
            var _this_1 = _super !== null && _super.apply(this, arguments) || this;
            _this_1.ServiceItemList = [];
            _this_1.Id = 0;
            return _this_1;
        }
        ServiceItemList.prototype.created = function () {
            var _this = this;
            _this.GetServiceItemList();
        };
        ServiceItemList.prototype.CreateSpaceItem = function () {
            var _this = this;
            _this.Id += 1;
            var spaceItem = {
                ServiceItemId: _this.Id,
                ServiceItemName: '',
                ImageName: 'NoImage.png',
                CreateTime: moment().toDate(),
                CreateUser: '',
                UpdateTime: moment().toDate(),
                UpdateUser: ''
            };
            _this.ServiceItemList.push(spaceItem);
        };
        ServiceItemList.prototype.GetServiceItemList = function () {
            var _this = this;
            service_1.default.GetServiceItemList().then(function (res) {
                if (!res.Success) {
                }
                if (res.Data) {
                    _this.ServiceItemList = res.Data;
                }
            }).catch(function (err) {
                console.log(err);
            });
        };
        ServiceItemList = __decorate([
            vue_property_decorator_1.Component({
                template: '#ServiceItemList',
                components: {
                    'service-item-list-item': ServiceItemListItem_1.default
                }
            })
        ], ServiceItemList);
        return ServiceItemList;
    }(vue_property_decorator_1.Vue));
    exports.default = ServiceItemList;
});
//# sourceMappingURL=ServiceItemList.js.map