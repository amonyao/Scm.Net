<template>
    <sc-panel :title="title" :remark="remark">
        <div ref="editor" class="editor"></div>
        <template #footer>
            <el-card shadow="hover">
                <el-row :gutter="15">
                    <el-col :span="16" :xs="24">
                        编程语言：<sc-select v-model="format" :data="format_list"></sc-select>
                        缩进字符：<sc-select v-model="indentChar" :data="indent_list"></sc-select>
                        缩进数量：<el-input-number v-model="indentQty" :min="1" :max="8" />
                    </el-col>
                    <el-col :span="8" :xs="24" style="text-align: right;">
                        <el-button type="primary" @click="minimize">压缩</el-button>
                        <el-button type="primary" @click="maximize">美化</el-button>
                    </el-col>
                </el-row>
            </el-card>
        </template>
    </sc-panel>
</template>

<script>
import { EditorView, basicSetup } from "codemirror";
import { EditorState } from "@codemirror/state";
import { javascript } from "@codemirror/lang-javascript";
import { json } from "@codemirror/lang-json";
import { xml } from "@codemirror/lang-xml";
import { css } from "@codemirror/lang-css";
import { sql } from "@codemirror/lang-sql";
import { vkbeautify } from "vkbeautify";

export default {
    name: 'tools_beautify',
    data() {
        return {
            title: '代码美化',
            remark: '在线代码美化工具，支持JSON、XML、SQL、CSS等编程语言。',
            format_list: [{ 'id': '1', 'label': '请选择', 'value': '0' },
            { 'id': '2', 'label': 'JSON', 'value': 'json' },
            { 'id': '3', 'label': 'XML', 'value': 'xml' },
            { 'id': '4', 'label': 'SQL', 'value': 'sql' },
            { 'id': '5', 'label': 'CSS', 'value': 'css' }],
            indent_list: [{ 'id': '1', 'label': '空格( )', 'value': ' ' },
            { 'id': '2', 'label': '句点(.)', 'value': '.' },
            { 'id': '3', 'label': '横线(-)', 'value': '-' },
            { 'id': '4', 'label': '星号(*)', 'value': '*' }],
            code: '',
            indentQty: 4,
            indentChar: ' ',
            format: 'json',
            minify: false,
            editor: null
        };
    },
    mounted() {
        const jsonString = `{'name':'john','age':23,
        
'hobby':['basketball','music'],
        
'address':{'city':'Beijing','country':'China'}}`

        let startState = EditorState.create({
            doc: jsonString,
            extensions: [basicSetup, javascript(), json(), xml(), css(), sql()]
        })

        this.editor = new EditorView({
            state: startState,
            parent: this.$refs.editor
        })
    },
    methods: {
        getValue() {
            return this.editor.state.doc.toString();
        },
        setValue(value) {
            this.editor.dispatch({ changes: { from: 0, to: this.editor.state.doc.length, insert: value } });
        },
        minimize() {
            this.minify = true;
            this.code = this.getValue();
            this.beautify(this.code);
        },
        maximize() {
            this.minify = false;
            this.code = this.getValue();
            this.beautify(this.code);
        },
        beautify(code) {
            var indent = this.$TOOL.lpad('', this.indentQty, this.indentChar || ' ');

            try {
                if (this.format == '0') {
                    this.$message.error('请选择语言格式！');
                    return;
                }
                if (this.format == 'json') {
                    if (typeof code == 'string') {
                        code = this.minify ? this.minifyJson(code) : this.formatJson(code, indent);
                    } else {
                        code = vkbeautify.json(code);
                    }

                    this.setValue(code);
                    return true;
                }
                if (this.format == 'xml') {
                    code = this.minify ? vkbeautify.xmlmin(code) : vkbeautify.xml(code, indent);
                    this.setValue(code);
                    return true;
                }
                if (this.format == 'css') {
                    code = this.minify ? vkbeautify.cssmin(code) : vkbeautify.css(code, indent);
                    this.setValue(code);
                    return true;
                }
                if (this.format == 'sql') {
                    code = this.minify ? vkbeautify.sqlmin(code) : vkbeautify.sql(code, indent);
                    this.setValue(code);
                    return true;
                }
            } catch (e) {
                this.$message.error(e.message);
                return false;
            }
        },
        minifyJson(jsonStr) {
            var result = '';
            var inQuotationMarks = false;
            var quotationKey = '';
            var lastChar = null;
            var isCmd = false;

            for (var i = 0; i < jsonStr.length; i++) {
                var currentChar = jsonStr.charAt(i);

                switch (currentChar) {
                    case '\n':
                    case this.indentChar:
                        if (inQuotationMarks) {
                            result += currentChar;
                        }
                        break;
                    case '"':
                        if (!inQuotationMarks) {
                            isCmd = lastChar !== '\\';
                            inQuotationMarks = isCmd;
                            quotationKey = currentChar;
                            result += currentChar;
                        } else {
                            isCmd = lastChar !== '\\' && currentChar == quotationKey;
                            if (isCmd) {
                                inQuotationMarks = false;
                            }
                            result += (isCmd ? '' : '\\') + currentChar;
                        }
                        break;
                    case '\'':
                        if (!inQuotationMarks) {
                            isCmd = lastChar !== '\\';
                            inQuotationMarks = isCmd;
                            quotationKey = currentChar;
                            result += isCmd ? '"' : currentChar;
                        } else {
                            isCmd = lastChar !== '\\' && currentChar == quotationKey;
                            if (isCmd) {
                                inQuotationMarks = false;
                            }
                            result += isCmd ? '"' : currentChar;
                        }
                        break;
                    default:
                        result += currentChar;
                        break;
                }

                lastChar = currentChar;
            }

            return result;
        },
        formatJson(jsonStr, indent) {
            var result = '';
            var level = 0;
            var inQuotationMarks = false;
            var quotationKey = '';
            var lastChar = null;
            var isCmd = false;
            var isNewLine = false;

            for (var i = 0; i < jsonStr.length; i++) {
                var currentChar = jsonStr.charAt(i);

                switch (currentChar) {
                    case '\n':
                        if (inQuotationMarks) {
                            result += currentChar;
                        }
                        break;
                    case this.indentChar:
                        if (inQuotationMarks || (!isNewLine && lastChar != currentChar)) {
                            result += currentChar;
                        }
                        break;
                    case '{':
                    case '[':
                        if (!inQuotationMarks) {
                            isNewLine = true;
                            level += 1;
                        }
                        result += currentChar;
                        break;
                    case '}':
                    case ']':
                        level -= 1;
                        result += this.addIndent(level, indent);

                        if (!inQuotationMarks) {
                            isNewLine = true;
                        }
                        result += currentChar;
                        break;
                    case ',':
                        if (!inQuotationMarks) {
                            isNewLine = true;
                        }
                        result += currentChar;
                        break;
                    case ':':
                        if (!inQuotationMarks) {
                            result += ':';
                        } else {
                            result += currentChar;
                        }
                        break;
                    case '"':
                        if (isNewLine) {
                            result += this.addIndent(level, indent);
                            isNewLine = false;
                        }

                        if (!inQuotationMarks) {
                            isCmd = lastChar !== '\\';
                            inQuotationMarks = isCmd;
                            quotationKey = currentChar;
                            result += currentChar;
                        } else {
                            isCmd = lastChar !== '\\' && currentChar == quotationKey;
                            if (isCmd) {
                                inQuotationMarks = false;
                            }
                            result += (isCmd ? '' : '\\') + currentChar;
                        }
                        break;
                    case '\'':
                        if (isNewLine) {
                            result += this.addIndent(level, indent);
                            isNewLine = false;
                        }

                        if (!inQuotationMarks) {
                            isCmd = lastChar !== '\\';
                            inQuotationMarks = isCmd;
                            quotationKey = currentChar;
                            result += isCmd ? '"' : currentChar;
                        } else {
                            isCmd = lastChar !== '\\' && currentChar == quotationKey;
                            if (isCmd) {
                                inQuotationMarks = false;
                            }
                            result += isCmd ? '"' : currentChar;
                        }
                        break;
                    default:
                        if (isNewLine) {
                            result += this.addIndent(level, indent);
                            isNewLine = false;
                        }

                        result += currentChar;
                        break;
                }

                lastChar = currentChar;
            }

            return result;
        },
        addIndent(level, indent) {
            var result = '';
            if (!indent) {
                indent = '    ';
            }
            for (var i = 0; i < level; i++) {
                result += indent;
            }
            return '\n' + result;
        }
    }
}
</script>

<style scoped>
.editor {
    height: 100%;
    width: 100%;
    overflow: auto;
    background-color: #fff;
}

.el-select {
    width: 100px;
}

.el-input-number {
    width: 120px;
}
</style>

<style>
.cm-editor {
    height: 100%;
    width: 100%;
}
</style>