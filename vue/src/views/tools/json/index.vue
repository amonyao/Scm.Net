<template>
    <el-container>
        <el-main>
            <JsonEditorVue class="editor" v-model="data" @blur="validate" />
        </el-main>
    </el-container>
</template>
<script>
import JsonEditorVue from 'json-editor-vue3'

export default {
    name: 'Home',
    components: { JsonEditorVue },
    setup() {
        const data = { key: "key", value: "value" };
        const validate = async (editor) => {
            const res = await editor.validate();
            // res 是错误列表，如果是空数组，则表示检测没有错误
            console.log(res);
        };
        return {
            data,
            validate,
        };
    },
}
</script>
<style scoped>
.editor {
    height: 100%;
}
</style>