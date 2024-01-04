<template>
    <i :class="scIcon" :style="{ 'fontSize': scSize, 'color': color }">{{ name }}</i>
</template>
<script>
export default {
    props: {
        set: { type: String, defaul: 'sc' },//图标集：sc,md
        name: { type: String, defaul: '' },//图标名称
        size: { type: Number, defaul: 16 },//图标大小
        type: { type: String, defaul: 'Outlined' },//风格：Filled、Outlined、Round、Sharp
        color: { type: String, defaul: '#f00' },//颜色
        title: { type: String, defaul: '' },//提示
    },
    computed: {
        scIcon: function () {
            if (this.set == 'ms') {
                return 'material-symbols-' + this.type;
            }
            if (this.set == 'mi') {
                return 'material-icons-' + this.type;
            }

            if (!this.name || !this.name.startsWith('sc-')) {
                return 'scfont sc-menu-line';
            }
            return 'scfont ' + this.name;
        },
        scSize: function () {
            return this.size ? this.size + 'px' : '';
        }
    },
}
</script>