import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';
import LatestNewsEditManagement from './LatestNewsEditManagement';
import ToggleButton from 'vue-js-toggle-button'

Vue.use(ToggleButton);
Vue.use(BootstrapVue);

new Vue({
    render: (h => h(LatestNewsEditManagement))
}).$mount('#v_app');