<template>
    <div class="doc-content-wrapper" style="background-color: white;">
        <div class="doc-content-container">
            <div class="doc-content">
                <div>
                    <h1>h1标题</h1>
                    <p>内容</p>
                    <h2>h2标题</h2>
                    <div class="example">
                        <div class="example-showcase">
                        </div>
                        <el-divider style="margin: 0px;" />
                        <div class="op-btns">
                            <i class="el-icon op-btn el-tooltip__trigger el-tooltip__trigger" aria-label="复制代码" tabindex="0"
                                role="button" style="font-size: 16px;">
                                <svg preserveAspectRatio="xMidYMid meet" viewBox="0 0 24 24" width="1.2em" height="1.2em">
                                    <path fill="currentColor"
                                        d="M7 6V3a1 1 0 0 1 1-1h12a1 1 0 0 1 1 1v14a1 1 0 0 1-1 1h-3v3c0 .552-.45 1-1.007 1H4.007A1.001 1.001 0 0 1 3 21l.003-14c0-.552.45-1 1.007-1H7zM5.003 8L5 20h10V8H5.003zM9 6h8v10h2V4H9v2z">
                                    </path>
                                </svg>
                            </i>
                            <i class="el-icon op-btn el-tooltip__trigger el-tooltip__trigger" style="font-size: 16px;"
                                @click="viewCode">
                                <svg preserveAspectRatio="xMidYMid meet" viewBox="0 0 24 24" width="1.2em" height="1.2em">
                                    <path fill="currentColor"
                                        d="m23 12l-7.071 7.071l-1.414-1.414L20.172 12l-5.657-5.657l1.414-1.414L23 12zM3.828 12l5.657 5.657l-1.414 1.414L1 12l7.071-7.071l1.414 1.414L3.828 12z">
                                    </path>
                                </svg>
                            </i>
                        </div>
                        <div class="example-source-wrapper" v-show="codeVisible">
                            <div class="example-source language-vue">
                                <highlightjs language="JavaScript" :autodetect="false" :code="code"></highlightjs>
                            </div>
                        </div>
                        <div class="example-float-control" tabindex="0" role="button" v-show="codeVisible"
                            @click="viewCode">
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
import '@/assets/code.scss';

//import 'highlight.js/styles/atom-one-dark.css'
import 'highlight.js/styles/stackoverflow-light.css';
import 'highlight.js/lib/common';
import hljsVuePlugin from '@highlightjs/vue-plugin';

export default {
    components: {
        highlightjs: hljsVuePlugin.component
    },
    data() {
        return {
            code: `<template>
  <el-row class="mb-4">
    <el-button>Default</el-button>
    <el-button type="primary">Primary</el-button>
    <el-button type="success">Success</el-button>
    <el-button type="info">Info</el-button>
    <el-button type="warning">Warning</el-button>
    <el-button type="danger">Danger</el-button>
  </el-row>

  <el-row class="mb-4">
    <el-button plain>Plain</el-button>
    <el-button type="primary" plain>Primary</el-button>
    <el-button type="success" plain>Success</el-button>
    <el-button type="info" plain>Info</el-button>
    <el-button type="warning" plain>Warning</el-button>
    <el-button type="danger" plain>Danger</el-button>
  </el-row>

  <el-row class="mb-4">
    <el-button round>Round</el-button>
    <el-button type="primary" round>Primary</el-button>
    <el-button type="success" round>Success</el-button>
    <el-button type="info" round>Info</el-button>
    <el-button type="warning" round>Warning</el-button>
    <el-button type="danger" round>Danger</el-button>
  </el-row>

  <el-row>
    <el-button :icon="Search" circle />
    <el-button type="primary" :icon="Edit" circle />
    <el-button type="success" :icon="Check" circle />
    <el-button type="info" :icon="Message" circle />
    <el-button type="warning" :icon="Star" circle />
    <el-button type="danger" :icon="Delete" circle />
  </el-row>
</template>

&lt;script setup>
import {
  Check,
  Delete,
  Edit,
  Message,
  Search,
  Star,
} from "@element-plus/icons-vue"
< /script>`,
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