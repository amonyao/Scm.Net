<template>
    <sc-dialog v-model="visible" show-fullscreen destroy-on-close :title="title" width="750px" @close="close">
        <el-row>
            <el-col :span="8">
                <el-image style="width:240px; height: 240px;"></el-image>
            </el-col>
            <el-col :span="16">
                <el-form ref="formRef" label-width="100px" :model="formData">
                    <el-form-item v-for="(spec, index) in spec_list" :key="index" :label="spec.namec" :prop="spec.prop">
                        <el-radio-group v-model="spec.vid">
                            <el-radio border v-for="item in spec.values" :label="item.namec" :value="item.id"
                                :key="item.id" />
                        </el-radio-group>
                    </el-form-item>
                    <el-form-item label="数量" prop="barcode">
                        <el-input-number v-model="formData.qty" placeholder="请输入数量" :min="1"></el-input-number>
                    </el-form-item>
                </el-form>
            </el-col>
        </el-row>

        <template #footer>
            <span class="error">{{ error }}</span>
            <el-button @click="close">取 消</el-button>
            <el-button :loading="isSaveing" type="primary" @click="save">
                确 定
            </el-button>
        </template>
    </sc-dialog>
</template>
<script>
export default {
    emits: ['complete'],
    data() {
        return {
            title: '选择规格',
            visible: false,
            isSaveing: false,
            formData: this.def_data(),
            spu: {},
            spec_list: [],
            error: '',
        };
    },
    mounted() {
    },
    methods: {
        def_data() {
            return {
                id: '0',
                radio1: 'New York',
                qty: 1
            }
        },
        async open(spu) {
            this.spu = spu;
            
            // var res = await this.$API.posspu.edit.get(row.id);
            // this.formData = res.data;
            var spec = [
                {
                    'pid': '1',
                    'namec': '11',
                    'values': [
                        {
                            'vid': '11',
                            'namec': '11'
                        },
                        {
                            'vid': '12',
                            'namec': '12'
                        }
                    ]
                },
                {
                    'pid': '2',
                    'namec': '22',
                    'values': [
                        {
                            'vid': '21',
                            'namec': '21'
                        },
                        {
                            'vid': '22',
                            'namec': '22'
                        }
                    ]
                },
            ];
            for (var i = 0; i < spec.length; i += 1) {
                spec[i].vid = '';
                spec[i].prop = 'spec_' + i;
            }

            this.spec_list = spec;
            this.visible = true;
        },
        save() {
            if (!this.spec_list) {
                this.error = '无效的规格列表！';
                return;
            }
            var data = {};
            for (var i = 0; i < this.spec_list.length; i += 1) {
                var spec = this.spec_list[i];
                if (!spec.vid) {
                    this.error = '请选择' + spec.namec + '！';
                    return;
                }
                data.spec = '';
            }
            if (!this.formData.qty) {
                this.error = '请输入数量！';
                return;
            }
            data.qty = this.formData.qty;

            this.$refs.formRef.validate(async (valid) => {
                if (valid) {
                    this.isSaveing = true;

                    let res = await this.$API.possoorder.add.post(data);

                    this.isSaveing = false;
                    if (res.code == 200) {
                        this.$emit("complete");
                        this.visible = false;
                        this.$message.success("保存成功");
                    } else {
                        this.$alert(res.message, "提示", { type: "error" });
                    }
                }
            });
        },
        close() {
            this.formData = this.def_data();
            this.$refs.formRef.resetFields();
            this.visible = false;
        },
    },
};
</script>
<style scoped>
.error {
    color: red;
    margin-right: 1rem;
}
</style>