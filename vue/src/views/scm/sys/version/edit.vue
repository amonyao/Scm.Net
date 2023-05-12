<template>
    <sc-dialog v-model="visible" show-fullscreen destroy-on-close :title="titleMap[mode]" width="750px" @close="close">
        <el-form ref="formRef" label-width="100px" :model="formData" :rules="rules">
            <el-row>
                <el-col :span="12">
                    <el-form-item label="终端类型" prop="types">
                        <el-select v-model="formData.types" style="width: 100%;" placeholder="请选择">
                            <el-option v-for="item in typeList" :key="item.id" :label="item.label"
                                :value="item.value"></el-option>
                        </el-select>
                    </el-form-item>
                </el-col>
                <el-col :span="12">
                    <el-form-item label="系统代码" prop="sys">
                        <el-input v-model="formData.sys" placeholder="请输入" :maxlength="16" show-word-limit
                            clearable></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row>
                <el-col :span="12">
                    <el-form-item label="版本" prop="ver">
                        <el-input v-model="formData.ver" placeholder="请输入" :maxlength="16" show-word-limit
                            clearable></el-input>
                    </el-form-item>
                </el-col>
                <el-col :span="12">
                    <el-form-item label="最小版本" prop="ver_min">
                        <el-input v-model="formData.ver_min" placeholder="请输入联系人" :maxlength="16" show-word-limit
                            clearable></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row>
                <el-col :span="8">
                    <el-form-item label="是否内测" prop="alpha">
                        <el-switch v-model="formData.alpha" class="ml-2" />
                    </el-form-item>
                </el-col>
                <el-col :span="8">
                    <el-form-item label="是否公测" prop="beta">
                        <el-switch v-model="formData.beta" class="ml-2" />
                    </el-form-item>
                </el-col>
                <el-col :span="8">
                    <el-form-item label="强制更新" prop="forced">
                        <el-switch v-model="formData.forced" class="ml-2" />
                    </el-form-item>
                </el-col>
            </el-row>
            <el-form-item label="版本信息" prop="remark">
                <el-input v-model="formData.remark" placeholder="请输入手机号码" :maxlength="1024" show-word-limit
                    :autosize="{ minRows: 6, maxRows: 9 }" type="textarea" clearable></el-input>
            </el-form-item>
        </el-form>

        <template #footer>
            <el-button @click="close">取 消</el-button>
            <el-button :loading="isSaveing" type="primary" @click="save">
                确 定
            </el-button>
        </template>
    </sc-dialog>
</template>
<script>
export default {
    data() {
        return {
            mode: "add",
            titleMap: { add: "新增", edit: "编辑" },
            visible: false,
            isSaveing: false,
            typeList: [],
            formData: {
                id: 0,
                types: 0,
                sys: '',
                ver: '',
                ver_min: '',
                alpha: false,
                beta: false,
                forced: false,
                remark: ''
            },
            rules: {
                types: [
                    {
                        required: true,
                        trigger: "blur",
                        message: "请选择终端类型",
                    },
                ],
                sys: [
                    {
                        required: true,
                        trigger: "blur",
                        message: "请输入系统代码",
                    },
                ],
                ver: [
                    {
                        required: true,
                        trigger: "blur",
                        message: "请输入当前版本",
                    },
                ],
                remark: [
                    {
                        required: true,
                        trigger: "blur",
                        message: "请输入版本信息",
                    },
                ],
            },
        };
    },
    mounted() {
    },
    methods: {
        async open(row) {
            await this.$SCM.list_dic(this.typeList, 'client_type', false);
            if (!row || !row.id) {
                this.mode = "add";
            } else {
                this.mode = "edit";
                var res = await this.$API.sysversion.edit.get(row.id);
                this.formData = res.data;
            }
            this.visible = true;
        },
        save() {
            this.$refs.formRef.validate(async (valid) => {
                if (valid) {
                    this.isSaveing = true;
                    let res = null;
                    if (this.formData.id === 0) {
                        res = await this.$API.sysversion.add.post(this.formData);
                    } else {
                        res = await this.$API.sysversion.update.put(this.formData);
                    }
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
            this.formData = {
                id: 0,
                types: 0,
                sys: '',
                ver: '',
                ver_min: '',
                alpha: false,
                beta: false,
                forced: false,
                remark: ''
            };
            this.$refs.formRef.resetFields();
            this.visible = false;
        },
    },
};
</script>
