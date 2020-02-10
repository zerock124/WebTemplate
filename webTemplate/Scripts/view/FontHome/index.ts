import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';
import FontHomeManagement from './FontHomeManagement';
import ToggleButton from 'vue-js-toggle-button'

Vue.use(ToggleButton);
Vue.use(BootstrapVue);

new Vue({
    render: (h => h(FontHomeManagement))
}).$mount('#v_app');