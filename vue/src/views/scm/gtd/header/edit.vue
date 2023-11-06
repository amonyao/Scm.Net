<template>
	<el-drawer v-model="visible" :title="titleMap[mode]" @close="close">
		<el-form ref="formRef" label-width="100px" :model="formData" :rules="rules" style="margin: 10px;">
			<el-form-item label="标题" prop="title">
				<el-input v-model="formData.title" placeholder="请输入标题" :maxlength="256" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="优先级" prop="priority">
				<el-input v-model="formData.priority" placeholder="请输入优先级" :maxlength="4" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="提醒标识" prop="remind">
				<el-input v-model="formData.remind" placeholder="请输入提醒标识" :maxlength="4" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="表达式" prop="cron">
				<el-input v-model="formData.cron" placeholder="请输入表达式" :maxlength="128" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="提示方式" prop="notice">
				<el-input v-model="formData.notice" placeholder="请输入提示方式" :maxlength="20" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="备注" prop="remark">
				<el-input v-model="formData.remark" type="textarea" rows="6" placeholder="请输入备注" :maxlength="1024"
					show-word-limit clearable></el-input>
			</el-form-item>
		</el-form>

		<template #footer>
			<el-button @click="close">取 消</el-button>
			<el-button :loading="isSaveing" type="primary" @click="save">
				确 定
			</el-button>
		</template>
	</el-drawer>
</template>
<script>
export default {
	data() {
		return {
			mode: "add",
			titleMap: { add: "新增", edit: "编辑" },
			visible: false,
			isSaveing: false,
			formData: this.def_data(),
			rules: {
				codec: [
					{ required: true, trigger: "blur", message: "编码不能为空" },
				],
			},
		};
	},
	mounted() {
	},
	methods: {
		def_data() {
			return {
				id: '0',
				user_id: '0',
				cat_id: '0',
				title: '',
				remark: '',
				priority: '0',
				remind: '0',
				cron: '',
				notice: '0',
			}
		},
		async open(row) {
			if (!row || !row.id) {
				this.mode = "add";
			} else {
				this.mode = "edit";
				var res = await this.$API.scmgtdheader.edit.get(row.id);
				this.formData = res.data;
			}
			this.visible = true;
		},
		save() {
			this.$refs.formRef.validate(async (valid) => {
				if (valid) {
					this.isSaveing = true;
					let res = null;
					if (this.formData.id === '0') {
						res = await this.$API.scmgtdheader.add.post(this.formData);
					} else {
						res = await this.$API.scmgtdheader.update.put(this.formData);
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
			this.formData = this.def_data();
			this.$refs.formRef.resetFields();
			this.visible = false;
		},
	},
};
</script>

<style>
.el-select {
	width: 100%;
}
</style>