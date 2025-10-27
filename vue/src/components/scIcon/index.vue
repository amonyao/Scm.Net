<!--
 * @Descripttion: scIcon图标组件
 * 所有图标的名称，必需以特定前缀开始，例如：sc-、vs-、ms-、mi-
 * @version: 1.1
 * @Author: zeroyao
 * @Date: 2025-10-21 10:32:41
-->
<template>
    <i :class="getScClass()" :style="{ 'color': color }" v-if="isSc()"></i>
    <i :class="getMsClass()" :style="{ 'fontSize': size + 'px', 'color': color }" v-else>{{ getMsName() }}</i>
</template>

<script>
export default {
    props: {
        name: { type: String, default: '' },//图标名称
        size: { type: Number, default: 16 },//图标大小
        type: { type: String, default: 'Outlined' },//风格：Filled、Outlined、Round、Sharp
        color: { type: String, default: '' },//颜色
        title: { type: String, default: '' },//提示
    },
    data() {
        return {
            set_list: ['vs', 'sc', 'ms', 'mi'],//图标系列列表
        }
    },
    methods: {
        isSc() {
            var tmp = this.name.toLowerCase();
            return tmp.startsWith('sc-');
        },
        getScClass() {
            return 'scicon scfont ' + this.name;
        },
        getMsClass() {
            return 'material-symbols-' + (this.type || 'outlined').toLowerCase();
        },
        getMsName() {
            return this.name.substring(3);
        },
        getMiClass() {
            return 'material-icons-' + (this.type || 'outlined').toLowerCase();
        }
    }
}
</script>
