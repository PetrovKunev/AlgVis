class DataStructureVisualizer {
    constructor() {
        this.steps = [];
        this.currentStep = 0;
        this.isPlaying = false;
        this.playInterval = null;
        this.speed = 1000;
        this.currentType = '';
        
        this.initializeElements();
        this.bindEvents();
    }
    
    initializeElements() {
        this.dataStructureSelect = document.getElementById('datastructure-select');
        this.operationsInput = document.getElementById('operations-input');
        this.speedSlider = document.getElementById('ds-speed-slider');
        this.startBtn = document.getElementById('ds-start-btn');
        this.pauseBtn = document.getElementById('ds-pause-btn');
        this.resetBtn = document.getElementById('ds-reset-btn');
        this.prevStepBtn = document.getElementById('ds-prev-step');
        this.nextStepBtn = document.getElementById('ds-next-step');
        this.visualization = document.getElementById('datastructure-visualization');
        this.operationDescription = document.getElementById('operation-description');
        this.operationText = document.getElementById('operation-text');
        this.operationMessage = document.getElementById('operation-message');
        this.messageText = document.getElementById('message-text');
        this.progressBar = document.getElementById('ds-progress-bar');
        this.progressFill = document.getElementById('ds-progress-fill');
        this.dataStructureInfo = document.getElementById('datastructure-info');
        this.dataStructureName = document.getElementById('datastructure-name');
        this.dataStructureDescription = document.getElementById('datastructure-description');
        this.stepControls = document.getElementById('ds-step-controls');
        this.operationControls = document.getElementById('operation-controls');
        this.operationHelp = document.getElementById('operation-help');
        this.examplesContainer = document.getElementById('examples-container');
    }
    
    bindEvents() {
        this.startBtn.addEventListener('click', () => this.startVisualization());
        this.pauseBtn.addEventListener('click', () => this.pauseVisualization());
        this.resetBtn.addEventListener('click', () => this.resetVisualization());
        this.prevStepBtn.addEventListener('click', () => this.previousStep());
        this.nextStepBtn.addEventListener('click', () => this.nextStep());
        this.speedSlider.addEventListener('input', (e) => this.speed = parseInt(e.target.value));
        this.dataStructureSelect.addEventListener('change', (e) => this.onDataStructureChange(e.target.value));
        this.operationsInput.addEventListener('input', () => this.resetVisualization());
        
        document.addEventListener('keydown', (e) => {
            if (e.code === 'Space' && !e.target.matches('input, textarea')) {
                e.preventDefault();
                this.isPlaying ? this.pauseVisualization() : this.startVisualization();
            } else if (e.code === 'ArrowLeft') {
                this.previousStep();
            } else if (e.code === 'ArrowRight') {
                this.nextStep();
            }
        });
    }
    
    onDataStructureChange(type) {
        this.currentType = type;
        this.resetVisualization();
        
        if (type) {
            this.operationControls.classList.remove('hidden');
            this.showOperationHelp(type);
            this.showExamples(type);
        } else {
            this.operationControls.classList.add('hidden');
            this.operationHelp.classList.add('hidden');
            this.examplesContainer.innerHTML = '<div class="text-gray-500 text-sm">Изберете структура за да видите примери</div>';
        }
    }
    
    showOperationHelp(type) {
        // Hide all help sections
        document.getElementById('array-help').classList.add('hidden');
        document.getElementById('stack-help').classList.add('hidden');
        document.getElementById('queue-help').classList.add('hidden');
        
        // Show relevant help
        document.getElementById(`${type}-help`).classList.remove('hidden');
        this.operationHelp.classList.remove('hidden');
    }
    
    showExamples(type) {
        const examples = {
            array: [
                { name: 'Основни операции', operations: ['set 0 10', 'set 1 20', 'set 2 30', 'get 1', 'search 20'] },
                { name: 'Търсене в масив', operations: ['set 0 5', 'set 1 15', 'set 2 25', 'search 15', 'search 100'] },
                { name: 'Работа с индекси', operations: ['set 0 1', 'set 1 2', 'set 2 3', 'get 0', 'get 1', 'get 2'] }
            ],
            stack: [
                { name: 'Push и Pop', operations: ['push 10', 'push 20', 'push 30', 'pop', 'pop'] },
                { name: 'Peek операции', operations: ['push 5', 'peek', 'push 10', 'peek', 'pop', 'peek'] },
                { name: 'Empty check', operations: ['isEmpty', 'push 1', 'isEmpty', 'pop', 'isEmpty'] }
            ],
            queue: [
                { name: 'Enqueue и Dequeue', operations: ['enqueue 10', 'enqueue 20', 'enqueue 30', 'dequeue', 'dequeue'] },
                { name: 'Front операции', operations: ['enqueue 5', 'front', 'enqueue 10', 'front', 'dequeue', 'front'] },
                { name: 'Empty check', operations: ['isEmpty', 'enqueue 1', 'isEmpty', 'dequeue', 'isEmpty'] }
            ]
        };
        
        const typeExamples = examples[type] || [];
        this.examplesContainer.innerHTML = typeExamples.map(example => 
            `<button class="w-full text-left p-2 rounded-lg hover:bg-gray-50 border border-gray-200" 
                     onclick="loadOperations('${example.operations.join('\\n')}')">
                ${example.name}
            </button>`
        ).join('');
    }
    
    async startVisualization() {
        if (this.steps.length === 0) {
            await this.loadDataStructureSteps();
        }
        
        if (this.steps.length === 0) return;
        
        this.isPlaying = true;
        this.startBtn.classList.add('hidden');
        this.pauseBtn.classList.remove('hidden');
        this.stepControls.classList.remove('hidden');
        this.progressBar.classList.remove('hidden');
        this.operationDescription.classList.remove('hidden');
        
        this.playInterval = setInterval(() => {
            if (this.currentStep < this.steps.length) {
                this.showStep(this.currentStep);
                this.currentStep++;
                this.updateProgress();
            } else {
                this.pauseVisualization();
                showNotification('Визуализацията е завършена!', 'success');
            }
        }, this.speed);
    }
    
    pauseVisualization() {
        this.isPlaying = false;
        this.startBtn.classList.remove('hidden');
        this.pauseBtn.classList.add('hidden');
        
        if (this.playInterval) {
            clearInterval(this.playInterval);
            this.playInterval = null;
        }
    }
    
    resetVisualization() {
        this.pauseVisualization();
        this.steps = [];
        this.currentStep = 0;
        this.visualization.innerHTML = '<div class="text-center text-gray-500">Изберете структура и добавете операции</div>';
        this.operationDescription.classList.add('hidden');
        this.operationMessage.classList.add('hidden');
        this.progressBar.classList.add('hidden');
        this.stepControls.classList.add('hidden');
        this.dataStructureInfo.classList.add('hidden');
        this.updateProgress();
    }
    
    async loadDataStructureSteps() {
        const type = this.dataStructureSelect.value;
        const operationsText = this.operationsInput.value.trim();
        
        if (!type || !operationsText) {
            showNotification('Моля изберете структура и въведете операции', 'warning');
            return;
        }
        
        try {
            const operations = operationsText.split('\n').filter(op => op.trim());
            
            if (operations.length === 0) {
                showNotification('Моля въведете валидни операции', 'error');
                return;
            }
            
            const response = await fetch('/Home/GetDataStructureSteps', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    type: type,
                    operations: operations
                })
            });
            
            if (!response.ok) {
                throw new Error('Грешка при зареждане на стъпките');
            }
            
            const result = await response.json();
            this.steps = result.steps;
            this.showDataStructureInfo(result);
            this.showStep(0);
            
        } catch (error) {
            console.error('Error loading data structure steps:', error);
            showNotification('Грешка при зареждане на структурата', 'error');
        }
    }
    
    showDataStructureInfo(dataStructure) {
        this.dataStructureName.textContent = dataStructure.name;
        this.dataStructureDescription.textContent = dataStructure.description;
        this.dataStructureInfo.classList.remove('hidden');
    }
    
    showStep(stepIndex) {
        if (stepIndex < 0 || stepIndex >= this.steps.length) return;
        
        const step = this.steps[stepIndex];
        this.renderDataStructure(step.data, step.highlightIndex, this.currentType);
        this.operationText.textContent = step.description;
        
        if (step.message) {
            this.showMessage(step.message, step.status);
        } else {
            this.operationMessage.classList.add('hidden');
        }
        
        this.updateProgress();
    }
    
    renderDataStructure(data, highlightIndex, type) {
        this.visualization.innerHTML = '';
        
        if (!data || data.length === 0) {
            this.visualization.innerHTML = '<div class="text-center text-gray-500">Структурата е празна</div>';
            return;
        }
        
        const container = document.createElement('div');
        container.className = 'flex justify-center items-center min-h-32';
        
        if (type === 'stack') {
            container.className = 'flex flex-col-reverse justify-center items-center space-y-reverse space-y-2 min-h-32';
        } else if (type === 'queue') {
            container.className = 'flex justify-center items-center space-x-2 min-h-32';
        } else {
            container.className = 'flex justify-center items-center space-x-2 min-h-32';
        }
        
        data.forEach((value, index) => {
            const element = document.createElement('div');
            element.className = 'array-item';
            element.textContent = value;
            
            if (highlightIndex === index) {
                element.classList.add('current');
            }
            
            // Add labels for queue
            if (type === 'queue') {
                const wrapper = document.createElement('div');
                wrapper.className = 'flex flex-col items-center';
                
                if (index === 0) {
                    const label = document.createElement('div');
                    label.className = 'text-xs text-gray-500 mb-1';
                    label.textContent = 'Начало';
                    wrapper.appendChild(label);
                }
                
                wrapper.appendChild(element);
                
                if (index === data.length - 1) {
                    const label = document.createElement('div');
                    label.className = 'text-xs text-gray-500 mt-1';
                    label.textContent = 'Край';
                    wrapper.appendChild(label);
                }
                
                container.appendChild(wrapper);
            } else if (type === 'stack') {
                const wrapper = document.createElement('div');
                wrapper.className = 'flex items-center space-x-2';
                
                wrapper.appendChild(element);
                
                if (index === data.length - 1) {
                    const label = document.createElement('div');
                    label.className = 'text-xs text-gray-500';
                    label.textContent = '← Връх';
                    wrapper.appendChild(label);
                }
                
                container.appendChild(wrapper);
            } else {
                const wrapper = document.createElement('div');
                wrapper.className = 'flex flex-col items-center';
                
                const indexLabel = document.createElement('div');
                indexLabel.className = 'text-xs text-gray-500 mb-1';
                indexLabel.textContent = index;
                wrapper.appendChild(indexLabel);
                
                wrapper.appendChild(element);
                
                container.appendChild(wrapper);
            }
        });
        
        this.visualization.appendChild(container);
    }
    
    showMessage(message, status) {
        this.messageText.textContent = message;
        this.operationMessage.classList.remove('hidden');
        
        // Remove existing status classes
        this.operationMessage.firstElementChild.classList.remove('bg-success-100', 'text-success-800', 'bg-accent-100', 'text-accent-800', 'bg-warning-100', 'text-warning-800', 'bg-primary-100', 'text-primary-800');
        
        // Add appropriate status class
        switch (status) {
            case 'success':
                this.operationMessage.firstElementChild.classList.add('bg-success-100', 'text-success-800');
                break;
            case 'error':
                this.operationMessage.firstElementChild.classList.add('bg-accent-100', 'text-accent-800');
                break;
            case 'warning':
                this.operationMessage.firstElementChild.classList.add('bg-warning-100', 'text-warning-800');
                break;
            default:
                this.operationMessage.firstElementChild.classList.add('bg-primary-100', 'text-primary-800');
        }
    }
    
    nextStep() {
        if (this.currentStep < this.steps.length) {
            this.showStep(this.currentStep);
            this.currentStep++;
        }
    }
    
    previousStep() {
        if (this.currentStep > 0) {
            this.currentStep--;
            this.showStep(this.currentStep);
        }
    }
    
    updateProgress() {
        const progress = this.steps.length > 0 ? (this.currentStep / this.steps.length) * 100 : 0;
        this.progressFill.style.width = `${progress}%`;
        
        // Update step controls
        this.prevStepBtn.disabled = this.currentStep === 0;
        this.nextStepBtn.disabled = this.currentStep >= this.steps.length;
        
        if (this.prevStepBtn.disabled) {
            this.prevStepBtn.classList.add('opacity-50', 'cursor-not-allowed');
        } else {
            this.prevStepBtn.classList.remove('opacity-50', 'cursor-not-allowed');
        }
        
        if (this.nextStepBtn.disabled) {
            this.nextStepBtn.classList.add('opacity-50', 'cursor-not-allowed');
        } else {
            this.nextStepBtn.classList.remove('opacity-50', 'cursor-not-allowed');
        }
    }
}

// Initialize the visualizer when the page loads
document.addEventListener('DOMContentLoaded', () => {
    const visualizer = new DataStructureVisualizer();
    window.dsVisualizer = visualizer; // Make it globally accessible for debugging
});

// Helper function for loading predefined operations
function loadOperations(operations) {
    const input = document.getElementById('operations-input');
    if (input) {
        input.value = operations;
        // Trigger the input event to reset visualization
        input.dispatchEvent(new Event('input'));
    }
}