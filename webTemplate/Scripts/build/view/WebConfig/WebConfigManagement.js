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
        WebConfigManagement.prototype.updated = function () {
            this.setAutocomplete('input-address');
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
        WebConfigManagement.prototype.setAutocomplete = function (id) {
            var _this_1 = this;
            var input = document.getElementById(id);
            var autocomplete = new google.maps.places.Autocomplete(input);
            enableEnterKey(input);
            function enableEnterKey(input) {
                var _addEventListener = input.addEventListener;
                var addEventListenerWrapper = function (type, listener) {
                    if (type === "keydown") {
                        var _listener_1 = listener;
                        listener = function (event) {
                            var suggestionSelected = document.getElementsByClassName('pac-item-selected').length;
                            if (event.key === 'Enter' && !suggestionSelected) {
                                var e = new $.Event("keydown", { key: "ArrowDown", code: "ArrowDown", keyCode: 40 });
                                _listener_1.apply(input, [e]);
                            }
                            _listener_1.apply(input, [event]);
                        };
                    }
                    _addEventListener.apply(input, [type, listener]);
                };
                input.addEventListener = addEventListenerWrapper;
            }
            autocomplete.setFields(['address_components', 'geometry', 'icon', 'name', 'formatted_address']);
            autocomplete.setComponentRestrictions({ country: "tw" });
            autocomplete.addListener('place_changed', function () {
                _this_1.place_changed(autocomplete.getPlace());
            });
        };
        WebConfigManagement.prototype.place_changed = function (place) {
            console.log(place);
            if (!place.geometry) {
                this.$bvToast.toast('「' + place.name + '」並不再範圍內', {
                    title: '地點錯誤',
                    variant: 'danger',
                });
                return;
            }
            var address = '';
            if (place.address_components) {
                address = [
                    (place.address_components[6] && place.address_components[6].short_name || ''),
                    (place.address_components[4] && place.address_components[4].short_name || ''),
                    (place.address_components[3] && place.address_components[3].short_name || ''),
                    (place.address_components[1] && place.address_components[1].short_name || ''),
                    (place.address_components[0] && place.address_components[0].short_name || ''),
                ].join(' ');
            }
            if (this.WebConfigItem && place.formatted_address) {
                this.WebConfigItem.CompanyAddress = place.formatted_address;
                this.WebConfigItem.Lat = place.geometry.location.lat();
                this.WebConfigItem.Lng = place.geometry.location.lng();
            }
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