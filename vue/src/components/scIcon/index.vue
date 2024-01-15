<template>
    <i :class="scIcon" :style="{ 'fontSize': scSize, 'color': color }">{{ scName() }}</i>
</template>
<script>
export default {
    props: {
        set: { type: String, default: 'sc' },//图标集：sc,md
        name: { type: String, default: '' },//图标名称
        size: { type: Number, default: 0 },//图标大小
        type: { type: String, default: 'Outlined' },//风格：Filled、Outlined、Round、Sharp
        color: { type: String, default: '' },//颜色
        title: { type: String, default: '' },//提示
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
    methods: {
        scName() {
            if (this.set != 'sc') {
                return this.name;
            }
            return '';
        }
    }
}
</script>