var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
define(["require", "exports", "vue", "bootstrap-vue", "./AuthorityCreateManagement"], function (require, exports, vue_1, bootstrap_vue_1, AuthorityCreateManagement_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    vue_1 = __importDefault(vue_1);
    bootstrap_vue_1 = __importDefault(bootstrap_vue_1);
    AuthorityCreateManagement_1 = __importDefault(AuthorityCreateManagement_1);
    vue_1.default.use(bootstrap_vue_1.default);
    new vue_1.default({
        render: (function (h) { return h(AuthorityCreateManagement_1.default); })
    }).$mount('#v_app');
});
//# sourceMappingURL=index.js.map