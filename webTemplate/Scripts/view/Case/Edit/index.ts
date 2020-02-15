import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';
import CaseEditManagement from './CaseEditManagement';
import ToggleButton from 'vue-js-toggle-button'

Vue.use(ToggleButton);
Vue.use(BootstrapVue);

new Vue({
    render: (h => h(CaseEditManagement))
}).$mount('#v_app');