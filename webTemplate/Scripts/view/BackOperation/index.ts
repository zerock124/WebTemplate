import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';
import BackOperationManagement from './BackOperationManagement';
import ToggleButton from 'vue-js-toggle-button'

Vue.use(ToggleButton);
Vue.use(BootstrapVue);

new Vue({
    render: (h => h(BackOperationManagement))
}).$mount('#v_app');