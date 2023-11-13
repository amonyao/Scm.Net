<template>
	<sc-dialog v-model="visible" show-fullscreen destroy-on-close :title="titleMap[mode]" width="750px" @close="close">
		<el-form ref="formRef" label-width="100px" :model="formData" :rules="rules">
			<el-form-item label="队列编码" prop="codec">
				<el-input v-model="formData.codec" placeholder="请输入队列名称" :maxlength="32" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="队列名称" prop="namec">
				<el-input v-model="formData.namec" placeholder="请输入队列名称" :maxlength="32" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="显示排序" prop="od">
				<el-input-number v-model="formData.od" :max="100" :step="1"></el-input-number>
			</el-form-item>
			<el-form-item label="数字长度" prop="length">
				<el-input-number v-model="formData.length" :max="8" :step="1"></el-input-number>
			</el-form-item>
			<el-form-item label="号码前缀" prop="prefix">
				<el-input v-model="formData.prefix" placeholder="请输入号码前缀" :maxlength="4" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="队列说明" prop="remark">
				<el-input v-model="formData.remark" type="textarea" rows="5" placeholder="请输入队列说明" :maxlength="256"
					show-word-limit clearable></el-input>
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
			formData: this.def_data(),
			rules: {
				codec: [
					{ required: true, trigger: "blur", message: "队列编码不能为空" },
				],
				namec: [
					{ required: true, trigger: "blur", message: "队列名称不能为空" },
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
				header_id: '0',
				namec: '',
				remark: '',
				od: 0,
				prefix: '',
				length: 0,
			}
		},
		async open(row, type = "edit") {
			if (type == "add") {
				this.mode = type;
				this.formData.header_id = row.id;
				this.formData.tag = row.type;
			} else {
				this.mode = type;
				var res = await this.$API.scmqcsdetail.edit.get(row.id);
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
						res = await this.$API.scmqcsdetail.add.post(this.formData);
					} else {
						res = await this.$API.scmqcsdetail.update.put(this.formData);
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