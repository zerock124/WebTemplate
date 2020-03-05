import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';
import CaseManagement from './CaseManagement';
import ToggleButton from 'vue-js-toggle-button';

Vue.use(ToggleButton);
Vue.use(BootstrapVue);

new Vue({
    render: (h => h(CaseManagement))
}).$mount('#v_app');