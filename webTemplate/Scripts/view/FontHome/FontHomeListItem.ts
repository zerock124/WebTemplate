import { Vue, Component, Prop } from 'vue-property-decorator'
import { FontHomeViewModel } from './model'

@Component({
    template: '#FontHomeListItem'
})

export default class FontHomeListItme extends Vue {
    @Prop(Array) ListItem: FontHomeViewModel | undefined;

}