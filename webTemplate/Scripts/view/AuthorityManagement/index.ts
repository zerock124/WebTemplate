import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';
import AuthorityManagement from './AuthorityManagement';
import ToggleButton from 'vue-js-toggle-button'

Vue.use(ToggleButton);
Vue.use(BootstrapVue);

new Vue({
    render: (h => h(AuthorityManagement))
}).$mount('#v_app');