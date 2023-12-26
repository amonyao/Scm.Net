<template>
    <h2>{{ title }}</h2>
    <p>{{ desc }}</p>
    <div class="example">
        <div class="example-showcase">
            <slot></slot>
        </div>
        <el-divider style="margin: 0px;" />
        <div class="op-btns">
            <span class="scfont sc-file-copy-line op-btn" v-copy="code"></span>
            <span class="scfont sc-expand-left-right-line op-btn" @click="viewCode"></span>
        </div>
        <div class="example-source-wrapper" v-show="codeVisible">
            <div class="example-source language-vue">
                <highlightjs language="JavaScript" :autodetect="false" :code="code"></highlightjs>
            </div>
        </div>
        <div class="example-float-control" tabindex="0" role="button" v-show="codeVisible" @click="viewCode">
            <span class="scfont sc-arrow-drop-up-fill" style="font-size: 16px;"></span><span>隐藏源代码</span>
        </div>
    </div>
</template>
<script>
import 'highlight.js/styles/stackoverflow-light.css';
import 'highlight.js/lib/common';
import hljsVuePlugin from '@highlightjs/vue-plugin';

export default {
    components: {
        highlightjs: hljsVuePlugin.component
    },
    props: {
        title: { type: String, default: '' },
        desc: { type: String, default: '' },
        warning: { type: String, default: '' },
        tips: { type: String, default: '' },
        code: { type: String, default: '' },
    },
    data() {
        return {
            codeVisible: false,
        }
    },
    mounted() {
    },
    methods: {
        viewCode() {
            this.codeVisible = !this.codeVisible;
        },
        copyCode() {

        }
    },
}
</script>
<style>
.example-showcase {
    padding: 1.5rem;
    margin: 0.5px;
    background-color: var(--el-bg-color);
}

.language-vue {
    margin: 0;
    border-radius: 0;
}

.example {
    border: 1px solid var(--el-border-color);
    border-radius: var(--el-border-radius-base);
}

.example .op-btns {
    padding: 0.5rem;
    display: flex;
    align-items: center;
    justify-content: flex-end;
    height: 2.5rem;
}

.example .op-btns .el-icon:hover {
    color: var(--el-text-color-primary);
}

.example .op-btns .op-btn {
    margin: 0 0.5rem;
    cursor: pointer;
    color: var(--el-text-color-secondary);
    transition: 0.2s;
    font-size: 16px;
}

.example .op-btns .op-btn.github a {
    transition: 0.2s;
    color: var(--el-text-color-secondary);
}

.example .op-btns .op-btn.github a:hover {
    color: var(--el-text-color-primary);
}

.example-float-control {
    display: flex;
    align-items: center;
    justify-content: center;
    border-top: 1px solid var(--el-border-color);
    height: 44px;
    box-sizing: border-box;
    background-color: var(--el-bg-color, #fff);
    border-bottom-left-radius: 4px;
    border-bottom-right-radius: 4px;
    margin-top: -1px;
    color: var(--el-text-color-secondary);
    cursor: pointer;
    position: sticky;
    left: 0;
    right: 0;
    bottom: 0;
    z-index: 10;
}

.example-float-control span {
    font-size: 14px;
    margin-left: 10px;
}

.example-float-control:hover {
    color: var(--el-color-primary);
}
</style>