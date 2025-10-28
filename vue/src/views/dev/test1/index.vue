<template>
    <el-container>
        <el-main>
            <el-row>
                <el-col :span="12">
                    <el-input type="textarea" v-model="input" :rows="10" />
                </el-col>
                <el-col :span="12">
                    <pre><code>{{ code }}</code></pre>
                </el-col>
            </el-row>
            <el-button type="primary" @click="format">格式化</el-button>
        </el-main>
    </el-container>
</template>
<script>
import vkbeautify from "vkbeautify";
import hljs from 'highlight.js/lib/core';
import 'highlight.js/styles/default.css';
import xml from "highlight.js/lib/languages/xml";
hljs.registerLanguage("xml", xml);
import json from "highlight.js/lib/languages/json";
hljs.registerLanguage("json", json);

export default {
    name: 'tools_code',
    data() {
        return {
            input: '',
            code: '',
            isString: false,
        };
    },
    methods: {
        format() {
            this.code = this.jsonFormat(this.input);
            document.querySelectorAll("pre code").forEach((el) => {
                hljs.highlightElement(el);
            });
        },
        jsonFormat(jsonTemp) {
            try {
                // stringify 时需指定缩进否则不会显示换行。为了防止传入的string没有指定 在此统一执行一遍
                if (typeof jsonTemp == 'string') {
                    jsonTemp = JSON.parse(jsonTemp);
                }

                var json = JSON.stringify(jsonTemp, undefined, 2);

                let jsonObj = JSON.parse(json);
                if (typeof jsonObj === "object") {
                    this.isString = false;
                    return vkbeautify.json(jsonTemp);
                } else {
                    this.isString = true;
                    return jsonTemp;
                }
            } catch (e) {
                this.isString = true;
                return jsonTemp;
            }
        }
    }
}
</script>
<style scoped>
.editor {
    height: 100%;
}
</style>