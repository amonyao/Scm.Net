<template>
    <span :class="scIcon" :style="{ 'fontSize': scSize, 'color': color }"></span>
</template>
<script>
export default {
    props: {
        icon: { type: String, defaul: '' },
        size: { type: Number, defaul: 16 },
        fill: { type: Boolean, defaul: false },
        color: { type: String, defaul: '#f00' },
        title: { type: String, defaul: '' },
    },
    data() {
        return {
        }
    },
    computed: {
        scIcon: function () {
            console.log('scIcon:' + this.icon);
            if (!this.icon || !this.icon.startsWith('sc-')) {
                return 'scfont sc-menu-line';
            }
            return 'scfont ' + this.icon;
        },
        scSize: function () {
            return this.size ? this.size + 'px' : '';
        }
    },
}
</script>