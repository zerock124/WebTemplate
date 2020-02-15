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
define(["require", "exports", "vue-property-decorator", "./service", "./ServiceItemListItem", "moment", "./ServiceItemEvent"], function (require, exports, vue_property_decorator_1, service_1, ServiceItemListItem_1, moment, ServiceItemEvent_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    service_1 = __importDefault(service_1);
    ServiceItemListItem_1 = __importDefault(ServiceItemListItem_1);
    ServiceItemEvent_1 = __importDefault(ServiceItemEvent_1);
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
            ServiceItemEvent_1.default.$on('EmitServiceItem', _this.EmitServiceItem.bind(_this));
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
                UpdateUser: '',
                PhotoFile: null
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
                    var length = _this.ServiceItemList.length - 1;
                    _this.Id = _this.ServiceItemList[length].ServiceItemId;
                }
            }).catch(function (err) {
                console.log(err);
            });
        };
        ServiceItemList.prototype.EmitServiceItem = function (data) {
            var _this = this;
            if (_this.ServiceItemList) {
                var length_1 = _this.ServiceItemList.length;
                for (var i = 0; i < length_1; i++) {
                    if (_this.ServiceItemList[i].ServiceItemId == data.ServiceItemId) {
                        _this.ServiceItemList[i].PhotoFile = data.PhotoFile;
                        _this.ServiceItemList[i].ImageName = data.ImageName;
                        _this.ServiceItemList[i].ServiceItemName = data.ServiceItemName;
                    }
                }
            }
        };
        ServiceItemList.prototype.CreateFormDateList = function () {
            var _this = this;
            if (_this.ServiceItemList) {
                var _formdate = new FormData();
                var length_2 = _this.ServiceItemList.length;
                for (var i = 0; i < length_2; i++) {
                    var photoFile = null;
                    _formdate.append('model[' + i + '].ServiceItemId', _this.ServiceItemList[i].ServiceItemId.toString());
                    _formdate.append('model[' + i + '].ServiceItemName', _this.ServiceItemList[i].ServiceItemName);
                    _formdate.append('model[' + i + '].ImageName', _this.ServiceItemList[i].ImageName);
                    photoFile = _this.ServiceItemList[i].PhotoFile;
                    _formdate.append('model[' + i + '].PhotoFile', photoFile ? photoFile : '');
                }
                _this.CreateServiceItemList(_formdate);
            }
        };
        ServiceItemList.prototype.CreateServiceItemList = function (_formdate) {
            var _this = this;
            service_1.default.CreateServiceItemList(_formdate).then(function (res) {
                if (!res.Success) {
                    console.log(res);
                }
                if (res.Success) {
                    _this.$bvToast.toast("服務項目新增成功", {
                        title: '服務項目管理',
                        variant: 'success'
                    });
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