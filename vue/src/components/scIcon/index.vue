<template>
    <span :class="scIcon" :style="{ 'fontSize': scSize, 'color': color }"></span>
</template>
<script>
export default {
    props: {
        icon: { type: String, defaul: '' },
        myicon: { type: String, defaul: '' },
        size: { type: Number, defaul: 16 },
        fill: { type: Boolean, defaul: false },
        color: { type: String, defaul: '#f00' },
        title: { type: String, defaul: '' },
    },
    data() {
        return {
            scIcon: '',
            scSize: '',
        }
    },
    watch: {
        myicon(val) {
            this.scIcon = 'scfont ' + val;
            console.log('icon:' + val + ',scIcon:' + this.scIcon)
        },
        size(val) {
            this.scSize = val ? val + 'px' : '';
        }
    },
}
</script>