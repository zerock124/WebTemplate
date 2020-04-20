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
define(["require", "exports", "vue-property-decorator", "./StaticPageItem", "./service"], function (require, exports, vue_property_decorator_1, StaticPageItem_1, service_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    StaticPageItem_1 = __importDefault(StaticPageItem_1);
    service_1 = __importDefault(service_1);
    var StaticPageManagement = (function (_super) {
        __extends(StaticPageManagement, _super);
        function StaticPageManagement() {
            var _this_1 = _super !== null && _super.apply(this, arguments) || this;
            _this_1.StaticPageList = [];
            _this_1.CompanyProfile = null;
            _this_1.TechnologyDevelopment = null;
            _this_1.ContactMethod = null;
            _this_1.PrivacyPolicy = null;
            return _this_1;
        }
        StaticPageManagement.prototype.created = function () {
            var _this = this;
            _this.GetStaticPageList();
        };
        StaticPageManagement.prototype.GetStaticPageList = function () {
            var _this = this;
            service_1.default.GetStaticPageList().then(function (res) {
                if (res.Success) {
                }
                if (res.Data) {
                    _this.StaticPageList = res.Data;
                    var length_1 = _this.StaticPageList.length;
                    for (var i = 0; i < length_1; i++) {
                        switch (_this.StaticPageList[i].StaticPageEnum) {
                            case 0:
                                _this.CompanyProfile = _this.StaticPageList[i];
                                break;
                            case 1:
                                _this.TechnologyDevelopment = _this.StaticPageList[i];
                                break;
                            case 2:
                                _this.ContactMethod = _this.StaticPageList[i];
                                break;
                            case 3:
                                _this.PrivacyPolicy = _this.StaticPageList[i];
                                break;
                            default:
                        }
                    }
                }
            }).catch(function (err) {
                console.log(err);
            });
        };
        StaticPageManagement = __decorate([
            vue_property_decorator_1.Component({
                template: '#StaticPageManagement',
                components: {
                    'company-profile': StaticPageItem_1.default,
                    'technology-development': StaticPageItem_1.default,
                    'contact-method': StaticPageItem_1.default,
                    'privacy-policy': StaticPageItem_1.default
                }
            })
        ], StaticPageManagement);
        return StaticPageManagement;
    }(vue_property_decorator_1.Vue));
    exports.default = StaticPageManagement;
});
//# sourceMappingURL=StaticPageManagement.js.map