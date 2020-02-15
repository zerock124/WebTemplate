import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';
import LatestNewsManagement from './LatestNewsManagement';
import ToggleButton from 'vue-js-toggle-button'

Vue.use(ToggleButton);
Vue.use(BootstrapVue);

new Vue({
    render: (h => h(LatestNewsManagement))
}).$mount('#v_app');