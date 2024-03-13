<template>
    <i class="scicon" :class="scIcon" :style="{ 'fontSize': scSize, 'color': color }">{{ scName() }}</i>
</template>

<script>
export default {
    props: {
        set: { type: String, default: 'ms' },//图标集：sc,md
        name: { type: String, default: '' },//图标名称
        size: { type: Number, default: 16 },//图标大小
        type: { type: String, default: 'Outlined' },//风格：Filled、Outlined、Round、Sharp
        color: { type: String, default: '' },//颜色
        title: { type: String, default: '' },//提示
    },
    computed: {
        scIcon: function () {
            if (this.name.startsWith('sc-')) {
                return 'scfont ' + this.name;
            }

            if (this.set == 'ms') {
                return 'material-symbols-' + (this.type || 'Outlined').toLowerCase();
            }
            if (this.set == 'mi') {
                return 'material-icons-' + (this.type || 'Outlined').toLowerCase();
            }

            // if (!this.name || !this.name.startsWith('sc-')) {
            //     return 'scfont sc-menu-line';
            // }
            // return 'scfont ' + this.name;
            return 'material-symbols-' + (this.type || 'Outlined').toLowerCase();
        },
        scSize: function () {
            if (this.name.startsWith('sc-')) {
                return '';
            }
            return this.size ? this.size + 'px' : '';
        }
    },
    methods: {
        scName() {
            // if (this.set != 'sc') {
            //     return this.name;
            // }
            // return '';
            if (this.name.startsWith('sc-')) {
                return '';
            }
            return this.name;
        }
    }
}
</script>