<template>
    <sc-panel :title="title" :remark="remark">
        <el-row>
            <el-col>
                <el-input v-model="param.input" type="textarea" rows="20"></el-input>
            </el-col>
        </el-row>
        <el-row style="margin: 15px 0px;">
            <el-col :span="4">
                <sc-select v-model="param.alg" :data="algList"></sc-select>
            </el-col>
            <el-col :span="20" style="text-align: right;">
                <el-radio-group v-model="param.letter" v-show="param.binary == 16">
                    <el-radio-button label="lower">小写</el-radio-button>
                    <el-radio-button label="upper">大写</el-radio-button>
                </el-radio-group>
                <sc-select v-model="param.sep" :data="sepList" style="margin: 0px 10px;width: 100px;"
                    v-show="param.binary == 16"></sc-select>
                <el-radio-group v-model="param.binary">
                    <el-radio-button label="16">16进制</el-radio-button>
                    <el-radio-button label="64">64进制</el-radio-button>
                </el-radio-group>
            </el-col>
        </el-row>
        <el-row>
            <el-col>
                <el-input v-model="result" readonly></el-input>
            </el-col>
        </el-row>
        <el-row>
            <el-col>
                <el-button @click="digest">计算</el-button>
            </el-col>
        </el-row>
    </sc-panel>
</template>
<script>
import CryptoJS from "crypto-js";
export default {
    name: 'Digest',
    data() {
        return {
            title: '消息摘要',
            remark: '这是一个文本摘要工具',
            algList: [
                { id: 'MD5', label: 'MD5' },
                { id: 'SHA1', label: 'SHA1' },
                { id: 'SHA2-224', label: 'SHA2-224' },
                { id: 'SHA2-256', label: 'SHA2-256' },
                { id: 'SHA2-384', label: 'SHA2-384' },
                { id: 'SHA2-512', label: 'SHA2-512' },
                { id: 'SHA3-224', label: 'SHA3-224' },
                { id: 'SHA3-256', label: 'SHA3-256' },
                { id: 'SHA3-384', label: 'SHA3-384' },
                { id: 'SHA3-512', label: 'SHA3-512' },
                { id: 'RIPEMD160', label: 'RIPEMD160' },],
            sepList: [
                { id: '', label: '无' },
                { id: ' ', label: '空格( )' },
                { id: ':', label: '冒号(:)' },
                { id: '-', label: '中横(-)' }],
            param: {
                input: '',
                alg: 'MD5',
                binary: 16,
                sep: '',
                letter: 'lower',
            },
            result: ''
        };
    },
    mounted() {

    },
    methods: {
        digest() {
            if (!this.param.input) {
                this.result = '';
                return;
            }

            var hash = '';
            switch (this.param.alg) {
                case 'MD5':
                    hash = CryptoJS.MD5(this.param.input);
                    break;
                case 'SHA1':
                    hash = CryptoJS.SHA1(this.param.input);
                    break;
                case 'SHA2-224':
                    hash = CryptoJS.SHA256(this.param.input);
                    break;
                case 'SHA2-256':
                    hash = CryptoJS.SHA256(this.param.input);
                    break;
                case 'SHA2-384':
                    hash = CryptoJS.SHA384(this.param.input);
                    break;
                case 'SHA2-512':
                    hash = CryptoJS.SHA512(this.param.input);
                    break;
                case 'SHA3-224':
                    hash = CryptoJS.SHA3(this.param.input, { outputLength: 224 });
                    break;
                case 'SHA3-256':
                    hash = CryptoJS.SHA3(this.param.input, { outputLength: 256 });
                    break;
                case 'SHA3-384':
                    hash = CryptoJS.SHA3(this.param.input, { outputLength: 384 });
                    break;
                case 'SHA3-512':
                    hash = CryptoJS.SHA3(this.param.input, { outputLength: 512 });
                    break;
                case 'RIPEMD':
                    hash = CryptoJS.RIPEMD160(this.param.input);
                    break;
            }

            if (this.param.binary == 64) {
                this.result = hash.toString(CryptoJS.enc.Base64);
                return;
            }

            var map = this.param.letter == 'upper'
                ? ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F']
                : ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f'];
            var words = hash.words;
            var sigBytes = words.length;
            var chars = [];
            for (var i = 0; i < sigBytes; i += 1) {
                var word = words[i];
                var tmp = map[(word >>> 28) & 0xf] + map[(word >>> 24) & 0xf];
                chars.push(tmp);

                tmp = map[(word >>> 20) & 0xf] + map[(word >>> 16) & 0xf];
                chars.push(tmp);

                tmp = map[(word >>> 12) & 0xf] + map[(word >>> 8) & 0xf];
                chars.push(tmp);

                tmp = map[(word >>> 4) & 0xf] + map[(word) & 0xf];
                chars.push(tmp);
            }
            this.result = chars.join(this.param.sep);
        }
    },
}
</script>
<style>
.el-radio-group {
    font-size: inherit;
}
</style>