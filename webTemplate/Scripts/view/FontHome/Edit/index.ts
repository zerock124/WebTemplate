import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';
import FontHomeEditManagement from './FontHomeEditManagement';
import ToggleButton from 'vue-js-toggle-button'

Vue.use(ToggleButton);
Vue.use(BootstrapVue);

new Vue({
    render: (h => h(FontHomeEditManagement))
}).$mount('#v_app');