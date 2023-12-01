<template>
	<el-drawer v-model="visible" :title="titleMap[mode]" @close="close">
		<el-form ref="formRef" label-width="100px" :model="formData" :rules="rules" style="margin: 10px;">
			<el-form-item label="标题" prop="title">
				<el-input v-model="formData.title" placeholder="请输入标题" :maxlength="256" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="优先级" prop="priority">
				<sc-select v-model="formData.priority" :data="priority_list" placeholder="请输入优先级"></sc-select>
			</el-form-item>
			<el-form-item label="进度" prop="handle">
				<sc-select v-model="formData.handle" :data="handle_list" placeholder="请输入提示方式"></sc-select>
			</el-form-item>
			<el-form-item label="备注" prop="remark">
				<el-input v-model="formData.remark" type="textarea" rows="6" placeholder="请输入备注" :maxlength="1024"
					show-word-limit clearable></el-input>
			</el-form-item>
			<el-form-item label="提醒标识" prop="remind">
				<el-switch v-model="formData.remind" active-text="开启" />
			</el-form-item>
			<el-form-item label="表达式" prop="cron" v-if="formData.remind">
				<sc-cron v-model="formData.cron" placeholder="请输入Cron定时规则" clearable></sc-cron>
			</el-form-item>
			<el-form-item label="提示方式" prop="notice" v-if="formData.remind">
				<sc-select v-model="formData.notice" :data="notice_list" placeholder="请输入提示方式"></sc-select>
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
import scCron from "@/components/scCron";
export default {
	components: {
		scCron,
	},
	data() {
		return {
			mode: "add",
			titleMap: { add: "新增", edit: "编辑" },
			visible: false,
			isSaveing: false,
			formData: this.def_data(),
			rules: {
				title: [
					{ required: true, trigger: "blur", message: "标题不能为空" },
				],
				priority: [
					{ required: true, trigger: "blur", pattern: '^[1-9]+$', message: "优先级不能为空" },
				],
				handle: [
					{ required: true, trigger: "blur", pattern: '^[1-9]+$', message: "进度不能为空" },
				],
			},
			priority_list: [this.$SCM.OPTION_ONE],
			notice_list: [this.$SCM.OPTION_ONE],
			handle_list: [this.$SCM.OPTION_ONE]
		};
	},
	mounted() {
		this.$SCM.list_dic(this.priority_list, 'gtd_priority', false);
		this.$SCM.list_dic(this.notice_list, 'gtd_notice', false);
		this.$SCM.list_dic(this.handle_list, 'gtd_handle', false);
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
				handle: '1',
			}
		},
		async open(row) {
			if (!row || !row.id) {
				this.mode = "add";
			} else {
				this.mode = "edit";
				var res = await this.$API.scmgtdheader.edit.get(row.id);
				this.formData = res.data;
				this.formData.priority = '' + this.formData.priority;
				this.formData.notice = '' + this.formData.notice;
				this.formData.handle = '' + this.formData.handle;
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