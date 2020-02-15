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
define(["require", "exports", "vue-property-decorator", "../Share/Enums", "./ServiceItemEvent"], function (require, exports, vue_property_decorator_1, Enums_1, ServiceItemEvent_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    ServiceItemEvent_1 = __importDefault(ServiceItemEvent_1);
    var ServiceItemListItem = (function (_super) {
        __extends(ServiceItemListItem, _super);
        function ServiceItemListItem() {
            var _this_1 = _super !== null && _super.apply(this, arguments) || this;
            _this_1.image = '';
            _this_1.ImageName = '';
            _this_1.PhotoFile = null;
            _this_1.ServiceItemName = '';
            _this_1.DefaultImage = '';
            return _this_1;
        }
        ServiceItemListItem.prototype.created = function () {
            var _this = this;
            _this.GetDefaultFontHomeUrl();
        };
        ServiceItemListItem.prototype.GetDefaultFontHomeUrl = function () {
            var _this = this;
            if (_this.ListItem) {
                var BasePath = window.BasePath;
                var photo = _this.ListItem.ImageName;
                _this.DefaultImage = BasePath + Enums_1.UrlPathEnum.ServiceItemPhoto + '?filename=' + photo;
            }
        };
        ;
        ServiceItemListItem.prototype.fileSelected = function (event) {
            var file = event.target.files.item(0);
            this.ImageName = file.name;
            var reader = new FileReader();
            reader.addEventListener('load', this.imageLoader);
            reader.readAsDataURL(file);
        };
        ;
        ServiceItemListItem.prototype.imageLoader = function (event) {
            this.image = event.target.result;
        };
        ServiceItemListItem.prototype.OnServiceItemNameChange = function () {
            var _this = this;
            if (_this.ListItem) {
                var ServiceItem = {
                    ServiceItemId: _this.ListItem.ServiceItemId,
                    ImageName: _this.ImageName,
                    PhotoFile: _this.PhotoFile,
                    ServiceItemName: _this.ServiceItemName
                };
                ServiceItemEvent_1.default.$emit('EmitServiceItem', ServiceItem);
            }
        };
        ServiceItemListItem.prototype.OnListItemChange = function () {
            var _this = this;
            if (_this.ListItem) {
                var ServiceItem = {
                    ServiceItemId: _this.ListItem.ServiceItemId,
                    ImageName: _this.ImageName,
                    PhotoFile: _this.PhotoFile,
                    ServiceItemName: _this.ServiceItemName
                };
                ServiceItemEvent_1.default.$emit('EmitServiceItem', ServiceItem);
            }
        };
        __decorate([
            vue_property_decorator_1.Prop(Object)
        ], ServiceItemListItem.prototype, "ListItem", void 0);
        __decorate([
            vue_property_decorator_1.Watch('ServiceItemName')
        ], ServiceItemListItem.prototype, "OnServiceItemNameChange", null);
        __decorate([
            vue_property_decorator_1.Watch('PhotoFile')
        ], ServiceItemListItem.prototype, "OnListItemChange", null);
        ServiceItemListItem = __decorate([
            vue_property_decorator_1.Component({
                template: '#ServiceItemListItem'
            })
        ], ServiceItemListItem);
        return ServiceItemListItem;
    }(vue_property_decorator_1.Vue));
    exports.default = ServiceItemListItem;
});
//# sourceMappingURL=ServiceItemListItem.js.map