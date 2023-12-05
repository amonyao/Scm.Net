<template>
    <el-container>
        <el-main>
            <input type="textarea" v-model="code" />
            <button @click="format" />
        </el-main>
    </el-container>
</template>
<script>
import vkbeautify from "vkbeautify";

export default {
    name: 'Home',
    data() {
        return {
            code: '',
            isString: false,
        };
    },
    methods: {
        format() {

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