import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';
import CaseCreateManagement from './CaseCreateManagement';
import  ToggleButton  from 'vue-js-toggle-button'

Vue.use(ToggleButton);
Vue.use(BootstrapVue);

new Vue({
    render: (h => h(CaseCreateManagement))
}).$mount('#v_app');