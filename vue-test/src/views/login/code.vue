<template>
    <div class="doc-content-wrapper" style="background-color: white;">
        <div class="doc-content-container">
            <div class="doc-content">
                <div>
                    <div class="example">
                        <div class="example-showcase">
                        </div>
                        <div class="el-divider el-divider--horizontal m-0" role="separator"
                            style="--el-border-style: solid;"><!--v-if--></div>
                        <div class="op-btns">
                            <i class="el-icon op-btn el-tooltip__trigger el-tooltip__trigger" aria-label="复制代码" tabindex="0"
                                role="button" style="font-size: 16px;">
                                <svg preserveAspectRatio="xMidYMid meet" viewBox="0 0 24 24" width="1.2em" height="1.2em">
                                    <path fill="currentColor"
                                        d="M7 6V3a1 1 0 0 1 1-1h12a1 1 0 0 1 1 1v14a1 1 0 0 1-1 1h-3v3c0 .552-.45 1-1.007 1H4.007A1.001 1.001 0 0 1 3 21l.003-14c0-.552.45-1 1.007-1H7zM5.003 8L5 20h10V8H5.003zM9 6h8v10h2V4H9v2z">
                                    </path>
                                </svg>
                            </i>
                            <button aria-label="查看源代码"
                                class="reset-btn el-icon op-btn el-tooltip__trigger el-tooltip__trigger" @click="viewCode">
                                <i class="el-icon" style="font-size: 16px;">
                                    <svg preserveAspectRatio="xMidYMid meet" viewBox="0 0 24 24" width="1.2em"
                                        height="1.2em">
                                        <path fill="currentColor"
                                            d="m23 12l-7.071 7.071l-1.414-1.414L20.172 12l-5.657-5.657l1.414-1.414L23 12zM3.828 12l5.657 5.657l-1.414 1.414L1 12l7.071-7.071l1.414 1.414L3.828 12z">
                                        </path>
                                    </svg>
                                </i>
                            </button>
                        </div>
                        <div class="example-source-wrapper" v-show="codeVisible">
                            <div class="example-source language-vue">
                                <pre><code></code></pre>
                            </div>
                        </div>
                        <div class="example-float-control" tabindex="0" role="button" v-show="codeVisible">
                            <i class="el-icon" style="font-size: 16px;">
                                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 1024 1024">
                                    <path fill="currentColor" d="M512 320 192 704h639.936z"></path>
                                </svg>
                            </i><span>隐藏源代码</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    data() {
        return {
            codeVisible: false,
        };
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
};
</script>
<style>
.toc-wrapper {
    display: none;
    padding-left: 64px
}

.toc-wrapper .toc-content {
    position: sticky;
    top: calc(var(--header-height) + 32px);
    margin-top: 0;
    padding: 4px 8px 4px 12px;
    margin-bottom: 32px;
    width: 200px
}

.toc-wrapper .toc-content .toc-marker {
    opacity: 0;
    position: absolute;
    background-color: var(--brand-color);
    border-radius: 4px;
    width: 4px;
    height: 14px;
    top: 30px;
    left: 0;
    z-index: 0;
    transition: top .25s cubic-bezier(0, 1, .5, 1), opacity .25s, background-color .5s
}

.toc-wrapper .toc-content__heading {
    font-size: 12px;
    color: var(--text-color-light);
    font-weight: 600;
    text-transform: uppercase;
    margin-top: 0
}

.toc-wrapper .toc-content .toc-items {
    list-style: none;
    padding: 0;
    margin: 12px 0 0;
    line-height: 1.2
}

.toc-wrapper .toc-content .toc-items .toc-item {
    margin-top: 10px;
    font-size: 11px;
    color: var(--el-text-color-secondary);
    text-overflow: ellipsis;
    overflow: hidden;
    white-space: nowrap;
    color: inherit
}

.toc-wrapper .toc-content .toc-items .toc-item .toc-link {
    display: inline-block;
    position: relative;
    color: var(--el-text-color-secondary);
    transition: color var(--el-transition-duration)
}

.toc-wrapper .toc-content .toc-items .toc-item .toc-link.active {
    color: var(--brand-color)
}

.toc-wrapper .toc-content .toc-items .toc-item.subitem {
    padding-left: 1rem
}

@media screen and (min-width: 1440px) {
    .toc-wrapper {
        display: block
    }
}

@media screen and (min-width: 1680px) {
    .toc-wrapper {
        padding-left: 96px;
        display: block
    }
}

.doc-content-wrapper {
    --vp-content-width: 800px;
    padding: 32px 24px 96px
}

.doc-content-wrapper .doc-content-container .doc-content {
    width: 100%
}

@media screen and (min-width: 1440px) {
    .doc-content-wrapper .doc-content-container {
        width: var(--vp-content-width)
    }
}

@media screen and (min-width: 768px) {
    .doc-content-wrapper {
        padding: 48px 32px 42px
    }
}

@media screen and (min-width: 960px) {
    .doc-content-wrapper {
        padding: 64px 64px 42px
    }
}

@media screen and (min-width: 1440px) {
    .doc-content-wrapper {
        padding: 64px 0 42px 64px;
        display: flex
    }
}

@media screen and (min-width: 1680px) {
    .doc-content-wrapper {
        padding: 64px 0 42px 96px;
        display: flex
    }
}

.example-showcase {
    padding: 1.5rem;
    margin: .5px;
    background-color: var(--el-bg-color)
}

.language-vue {
    margin: 0;
    border-radius: 0
}

.example {
    border: 1px solid var(--el-border-color);
    border-radius: var(--el-border-radius-base)
}

.example .op-btns {
    padding: .5rem;
    display: flex;
    align-items: center;
    justify-content: flex-end;
    height: 2.5rem
}

.example .op-btns .el-icon:hover {
    color: var(--el-text-color-primary)
}

.example .op-btns .op-btn {
    margin: 0 .5rem;
    cursor: pointer;
    color: var(--el-text-color-secondary);
    transition: .2s
}

.example .op-btns .op-btn.github a {
    transition: .2s;
    color: var(--el-text-color-secondary)
}

.example .op-btns .op-btn.github a:hover {
    color: var(--el-text-color-primary)
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
    z-index: 10
}

.example-float-control span {
    font-size: 14px;
    margin-left: 10px
}

.example-float-control:hover {
    color: var(--el-color-primary)
}

button.reset-btn {
    background: none;
    border: none;
    padding: 0;
    margin: 0;
    cursor: pointer
}
</style>