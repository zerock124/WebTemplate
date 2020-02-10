import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';
import PermissionManagement from './PermissionManagement';

Vue.use(BootstrapVue);

new Vue({
    render: (h => h(PermissionManagement))
}).$mount('#v_app');