import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';
import ServiceItemManagement from './ServiceItemManagement';

Vue.use(BootstrapVue);

new Vue({
    render: (h => h(ServiceItemManagement))
}).$mount('#v_app');