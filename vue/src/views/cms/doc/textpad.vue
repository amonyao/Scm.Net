<template>
    <div class="textpad">
        <el-card><template #header>
                <div class="card-header">
                    <el-input v-model="formData.title"></el-input>
                </div>
            </template>
            <el-input v-model="formData.content" type="textarea" rows="30" class="content"></el-input>
        </el-card>
    </div>
    <el-button @click="save">保存</el-button>
</template>
<script>
export default {
    data() {
        return {
            formData: {}
        }
    },
    mounted() {
        this.init();
    },
    methods: {
        def_data() {
            return {
                id: '0',
                title: '',
                content: ''
            }
        },
        init() {
            this.formData = this.def_data();
        },
        async save() {
            var res = await this.$API.cmsdocnote.save.post(this.formData);
            if (!res || res.code != 200) {
                return;
            }
        }
    }
}
</script>
<style scoped>
.textpad {
    width: 860px;
    margin: 0 auto;

    .content {
    }
}
</style>