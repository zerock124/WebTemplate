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
define(["require", "exports", "vue-property-decorator", "./service"], function (require, exports, vue_property_decorator_1, service_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    service_1 = __importDefault(service_1);
    var WebConfigManagement = (function (_super) {
        __extends(WebConfigManagement, _super);
        function WebConfigManagement() {
            var _this_1 = _super !== null && _super.apply(this, arguments) || this;
            _this_1.WebConfigItem = null;
            _this_1.SaveForm = 'Loading';
            return _this_1;
        }
        WebConfigManagement.prototype.created = function () {
            var _this = this;
            _this.GetWebConfig();
        };
        WebConfigManagement.prototype.GetWebConfig = function () {
            var _this = this;
            service_1.default.GetWebConfig(1).then(function (res) {
                if (!res.Success) {
                    console.log(res);
                }
                if (res.Data) {
                    _this.WebConfigItem = res.Data;
                }
            }).catch(function (err) {
                console.log(err);
            });
        };
        WebConfigManagement.prototype.SubmitWebConfig = function () {
            var _this = this;
            _this.SaveForm = "Loading";
            _this.$bvModal.show("WebConfigModal");
            if (_this.WebConfigItem) {
                service_1.default.EditWebConfig(_this.WebConfigItem).then(function (res) {
                    if (!res.Success) {
                        console.log(res);
                        _this.SaveForm = "Error";
                    }
                    if (res.Success) {
                        _this.SaveSuccess();
                    }
                }).catch(function (err) {
                    console.log(err);
                    _this.SaveForm = "Error";
                });
            }
        };
        WebConfigManagement.prototype.SaveSuccess = function () {
            var _this = this;
            setTimeout(function () {
                _this.SaveForm = "Success";
            }, 1000);
        };
        WebConfigManagement.prototype.CloseModel = function () {
            var _this = this;
            _this.$bvModal.hide("WebConfigModal");
        };
        WebConfigManagement = __decorate([
            vue_property_decorator_1.Component({
                template: '#WebConfigManagement',
            })
        ], WebConfigManagement);
        return WebConfigManagement;
    }(vue_property_decorator_1.Vue));
    exports.default = WebConfigManagement;
});
//# sourceMappingURL=WebConfigManagement.js.map