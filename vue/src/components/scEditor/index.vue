<template>
	<div style="border: 1px solid #ccc">
		<div id="toolbar-container"></div>
		<div id="editor-container"></div>
	</div>
</template>
<script>
import '@wangeditor/editor/dist/css/style.css' // 引入 css

import { createEditor, createToolbar } from 'wang-editor'

export default {
	name: 'scEditor',
	emits: ['onchange'],
	props: {
		content: { type: String, default: '' }
	},
	data() {
		return {
			html: '',
		}
	},
	watch: {
		content(val) {
			this.scEditor.setHtml(!val ? "<p><br></p>" : val);
		}
	},
	mounted() {
		this.html = this.content;

		var editorConfig = {
			placeholder: '请输入内容...',
			onChange: function () {
				this.html = this.scEditor.getHtml();
				this.emits('onchange', this.html);
			},
			MENU_CONF: {
				// 配置上传图片
				uploadImage: {
					// 请求路径
					server: this.$API.cmsdocnote.upload.url,
					// 后端接收的文件名称
					fieldName: "file",
					maxFileSize: 1 * 1024 * 1024, // 1M
					// 上传的图片类型
					allowedFileTypes: ["image/*"],
					// 小于该值就插入 base64 格式（而不上传），默认为 0
					base64LimitSize: 1024, // 10MB
					customInsert: this.insertSuccess,
					// 携带的数据
					header: {
						token: ''
					},
					// 单个文件上传成功之后
					onSuccess: this.uploadSuccess,
					// 单个文件上传失败
					onFailed(file, res) {
						console.log(res)
						this.$message.error(`${file.name} 上传失败`)
					},
					// 上传错误，或者触发 timeout 超时
					onError(file, err, res) {
						console.log(err, res)
						this.$message.error(`${file.name} 上传出错`)
					},
				}
			}
		};
		this.scEditor = createEditor({
			selector: '#editor-container',
			config: editorConfig,
			mode: 'simple' // 或 default'simple' 
		})
		var toolbarConfig = {};
		createToolbar({
			editor: this.scEditor,
			selector: '#toolbar-container',
			config: toolbarConfig,
			mode: 'simple' // 或 'simple'
		});
	},
}
</script>
