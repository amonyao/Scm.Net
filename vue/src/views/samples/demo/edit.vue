<template>
	<sc-dialog v-model="visible" show-fullscreen :title="titleMap[mode]" width="700px" @close="close">
		<el-form ref="formRef" label-width="100px" :model="formData" :rules="rules">
			<el-form-item label="系统代码" prop="codes">
				<el-input v-model="formData.codes" disabled />
			</el-form-item>
			<el-form-item label="客户编码" prop="codec">
				<el-input v-model="formData.codec" clearable :maxlength="32" placeholder="请输入客户编码" show-word-limit />
			</el-form-item>
			<el-form-item label="客户全称" prop="namec">
				<el-input v-model="formData.namec" clearable :maxlength="128" placeholder="请输入客户全称" show-word-limit />
			</el-form-item>
			<el-form-item label="客户简称" prop="names">
				<el-input v-model="formData.names" clearable :maxlength="32" placeholder="请输入客户简称" show-word-limit />
			</el-form-item>
			<el-form-item label="电话" prop="phone">
				<el-input v-model="formData.phone" clearable :maxlength="32" placeholder="请输入电话" show-word-limit />
			</el-form-item>
			<el-form-item label="备注" prop="remark">
				<el-input v-model="formData.remark" :autosize="{ minRows: 2, maxRows: 4 }" :maxlength="500"
					placeholder="请输入备注" show-word-limit type="textarea" />
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
			formData: {
				id: '0',
				codec: '',
				names: '',
				phone: '',
				remark: ''
			},
			rules: {
				codec: [
					{ required: true, trigger: "blur", message: "客户编码不能为空", },
					{ required: true, pattern: /^[a-zA-Z0-9]{4,32}$/, message: "无效的客户编码", },
				],
				namec: [
					{ required: true, trigger: "blur", message: "客户全称不能为空", }
				],
			},
		};
	},
	mounted() {
	},
	methods: {
		async open(row) {
			if (!row || !row.id) {
				this.mode = "add";
			} else {
				this.mode = "edit";
				var res = await this.$API.samplesdemo.view.get(row.id);
				this.formData = res.data || {};
			}
			this.visible = true;
		},
		save() {
			this.$refs.formRef.validate(async (valid) => {
				if (valid) {
					this.isSaveing = true;
					let res = null;
					if (this.formData.id === '0') {
						res = await this.$API.samplesdemo.add.post(this.formData);
					} else {
						res = await this.$API.samplesdemo.save.put(this.formData);
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
				id: '0',
				option_id: 0,
				text: ''
			};
			this.$refs.formRef.resetFields();
			this.visible = false;
		},
	},
};
</script>
