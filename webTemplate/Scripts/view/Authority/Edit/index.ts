import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';
import AuthorityEditManagement from './AuthorityEditManagement';

Vue.use(BootstrapVue);

new Vue({
    render: (h => h(AuthorityEditManagement))
}).$mount('#v_app');