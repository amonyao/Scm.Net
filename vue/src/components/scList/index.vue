<template>
    <div>
        <div v-for="(item, index) in list" @click="onRightClick(item)"
            class="cm-flex cm-ai-fs cm-jc-sb cm-p-02 cm-border-bottom-eee">
            <div class="cm-flex cm-ai-fs cm-flex-1 cm-mr-02">
                <div class="cm-fs-030 cm-mr-04" :style="getWidth()">
                    <span class="cm-c-red cm-pt-01" v-if="item.require">*</span>{{ item.label }}
                </div>
                <span class="cm-mr-02 cm-c-999 cm-flex-1" v-if="item.icon || item.disabled">
                    <span v-if="item.prefixUnit">{{ item.prefixUnit }}</span>
                    {{ result[item.field] }}
                    <span v-if="item.suffixUnit">{{ item.suffixUnit }}</span></span>
                <input type="text" v-model="result[item.field]" class="item-input" v-if="!item.icon && !item.disabled">
            </div>
            <img :src="item.icon" alt="" class="cm-img-03" v-if="item.icon">
        </div>
    </div>
</template>
<script>
export default {
    name: "ElList",
    data() {
        return {
        }
    },
    //获取子组件传过来的激活tab
    props: {
        list: {
            type: Array, default: function () { return []; }
        },
        result: { type: Object, default: {} },
        width: { type: String, default: "" }
    },
    created() {
    },
    methods: {
        getWidth() {
            if (this.width) {
                return "width:" + this.width
            }
        },
        onRightClick(item) {
            if (item.onRightClick) {
                item.onRightClick();
            }
        }
    }
}
</script>
  
<style scoped>
.item-input {
    height: 30px;
    font-size: 0.3rem;
    flex: 1;
    border: none;
    outline: none;
    background: #fff;
}
</style>