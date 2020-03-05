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
define(["require", "exports", "vue-property-decorator", "../../Share/Enums", "moment", "vue2-editor", "../service"], function (require, exports, vue_property_decorator_1, Enums_1, moment, vue2_editor_1, service_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    vue2_editor_1 = __importDefault(vue2_editor_1);
    service_1 = __importDefault(service_1);
    vue_property_decorator_1.Vue.use(vue2_editor_1.default);
    var LatestNewsCreateManagement = (function (_super) {
        __extends(LatestNewsCreateManagement, _super);
        function LatestNewsCreateManagement() {
            var _this_1 = _super !== null && _super.apply(this, arguments) || this;
            _this_1.httpURL = window.location.href;
            _this_1.image = '';
            _this_1.PhotoFile = null;
            _this_1.DefaultImage = '';
            _this_1.ImageName = '';
            _this_1.LatestNewsEnum = 0;
            _this_1.Options = [{
                    value: 0,
                    text: '媒體報導'
                }];
            _this_1.StartDateTime = moment().startOf('day').format("YYYY-MM-DD HH:mm:ss");
            _this_1.LatestNewsTitle = '';
            _this_1.LatestNewsContent = '';
            _this_1.Remark = '';
            _this_1.Status = false;
            _this_1.customToolbar = [
                ["bold", "italic", "underline"],
                [{ list: "ordered" },
                    { list: "bullet" }]
            ];
            _this_1.SaveForm = 'Loading';
            return _this_1;
        }
        LatestNewsCreateManagement.prototype.created = function () {
            var _this = this;
            _this.GetDefaultFontHomeUrl();
        };
        LatestNewsCreateManagement.prototype.GetDefaultFontHomeUrl = function () {
            var _this = this;
            var BasePath = window.BasePath;
            var photo = "NoImage.jpg";
            _this.DefaultImage = BasePath + Enums_1.UrlPathEnum.CasePhoto + '?filename=' + photo;
        };
        ;
        LatestNewsCreateManagement.prototype.fileSelected = function (event) {
            var file = event.target.files.item(0);
            console.log(file);
            this.ImageName = file.name;
            var reader = new FileReader();
            reader.addEventListener('load', this.imageLoader);
            reader.readAsDataURL(file);
        };
        ;
        LatestNewsCreateManagement.prototype.imageLoader = function (event) {
            this.image = event.target.result;
        };
        LatestNewsCreateManagement.prototype.SetCreateLatestNews = function () {
            var _this = this;
            _this.$bvModal.show('LatestNewsModal');
            _this.SaveForm = 'Loading';
            var _a = this, PhotoFile = _a.PhotoFile, LatestNewsEnum = _a.LatestNewsEnum, StartDateTime = _a.StartDateTime, LatestNewsTitle = _a.LatestNewsTitle, LatestNewsContent = _a.LatestNewsContent, Remark = _a.Remark, Status = _a.Status;
            var _formdata = new FormData();
            _formdata.append('PhotoFile', PhotoFile ? PhotoFile : '');
            _formdata.append('LatestNewsEnum', LatestNewsEnum.toString());
            _formdata.append('StartDateTime', StartDateTime);
            _formdata.append('LatestNewsTitle', LatestNewsTitle);
            _formdata.append('LatestNewsContent', LatestNewsContent);
            _formdata.append('Remark', Remark);
            _formdata.append('Status', JSON.stringify(Status));
            _this.CreateFontHome(_formdata);
        };
        LatestNewsCreateManagement.prototype.CreateFontHome = function (data) {
            var _this = this;
            service_1.default.CreateLatestNews(data).then(function (res) {
                if (!res.Success) {
                    _this.SaveForm = 'Error';
                    console.log(res);
                }
                if (res.Data) {
                    _this.SaveForm = 'Success';
                }
            }).catch(function (err) {
                _this.SaveForm = 'Error';
                console.log(err);
            });
        };
        LatestNewsCreateManagement.prototype.HideModal = function () {
            var _this = this;
            _this.$bvModal.hide('LatestNewsModal');
        };
        LatestNewsCreateManagement.prototype.CloseModal = function () {
            var _this = this;
            _this.$bvModal.hide('LatestNewsModal');
            var locationURL = this.httpURL.split("/Create")[0];
            document.location.href = locationURL;
        };
        LatestNewsCreateManagement = __decorate([
            vue_property_decorator_1.Component({
                template: '#LatestNewsCreateManagement'
            })
        ], LatestNewsCreateManagement);
        return LatestNewsCreateManagement;
    }(vue_property_decorator_1.Vue));
    exports.default = LatestNewsCreateManagement;
});
//# sourceMappingURL=LatestNewsCreateManagement.js.map