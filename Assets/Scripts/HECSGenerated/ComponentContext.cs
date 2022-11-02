using Components;using System;using System.Runtime.CompilerServices;namespace HECSFramework.Core{	public partial class ComponentContext	{		public SavePositionComponent GetSavePositionComponent;		public ViewReferenceComponent GetViewReferenceComponent;		public AbilityOwnerComponent GetAbilityOwnerComponent;		public AbilityPredicateComponent GetAbilityPredicateComponent;		public ActorContainerID GetActorContainerID;		public AppVersionComponent GetAppVersionComponent;		public PoolableTagComponent GetPoolableTagComponent;		public PredicatesComponent GetPredicatesComponent;		public AnimatorStateComponent GetAnimatorStateComponent;		public CountersHolderComponent GetCountersHolderComponent;		public TestSerializationComponent GetTestSerializationComponent;		public TransformComponent GetTransformComponent;		public AIStrategyComponent GetAIStrategyComponent;		public UntilSuccessStrategyNodeComponent GetUntilSuccessStrategyNodeComponent;		public AbilitiesHolderComponent GetAbilitiesHolderComponent;		public AfterLifeCompleteTagComponent GetAfterLifeCompleteTagComponent;		public AfterLifeTagComponent GetAfterLifeTagComponent;		public GameLogicTagComponent GetGameLogicTagComponent;		public InputActionsComponent GetInputActionsComponent;		public InputListenerTagComponent GetInputListenerTagComponent;		public ItemTagComponent GetItemTagComponent;		public NavMeshAgentComponent GetNavMeshAgentComponent;		public NetworkEntityTagComponent GetNetworkEntityTagComponent;		public UIGroupTagComponent GetUIGroupTagComponent;		public UIViewReferenceComponent GetUIViewReferenceComponent;		public UnityRectTransformComponent GetUnityRectTransformComponent;		public UnityTransformComponent GetUnityTransformComponent;		public ViewDestructionDelayedComponent GetViewDestructionDelayedComponent;		public ViewReferenceGameObjectComponent GetViewReferenceGameObjectComponent;		public SoundVolumeComponent GetSoundVolumeComponent;		public AbilityTagComponent GetAbilityTagComponent;		public PassiveAbilityTag GetPassiveAbilityTag;		public ScenarioAnimationComponent GetScenarioAnimationComponent;		public StateContextComponent GetStateContextComponent;		public StateDataComponent GetStateDataComponent;		public StateInfoComponent GetStateInfoComponent;		public AnimationCheckOutsHolderComponent GetAnimationCheckOutsHolderComponent;		public OverrideAnimatorComponent GetOverrideAnimatorComponent;		public SetupAfterViewTagComponent GetSetupAfterViewTagComponent;		public AdditionalCanvasTagComponent GetAdditionalCanvasTagComponent;		public MainCanvasTagComponent GetMainCanvasTagComponent;		public UITagComponent GetUITagComponent;	}}namespace HECSFramework.Core{	public static class EntityComponentExtentions	{		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static SavePositionComponent GetSavePositionComponent(this IEntity entity)		{			return entity.ComponentContext.GetSavePositionComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static ViewReferenceComponent GetViewReferenceComponent(this IEntity entity)		{			return entity.ComponentContext.GetViewReferenceComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static AbilityOwnerComponent GetAbilityOwnerComponent(this IEntity entity)		{			return entity.ComponentContext.GetAbilityOwnerComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static AbilityPredicateComponent GetAbilityPredicateComponent(this IEntity entity)		{			return entity.ComponentContext.GetAbilityPredicateComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static ActorContainerID GetActorContainerID(this IEntity entity)		{			return entity.ComponentContext.GetActorContainerID;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static AppVersionComponent GetAppVersionComponent(this IEntity entity)		{			return entity.ComponentContext.GetAppVersionComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static PoolableTagComponent GetPoolableTagComponent(this IEntity entity)		{			return entity.ComponentContext.GetPoolableTagComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static PredicatesComponent GetPredicatesComponent(this IEntity entity)		{			return entity.ComponentContext.GetPredicatesComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static AnimatorStateComponent GetAnimatorStateComponent(this IEntity entity)		{			return entity.ComponentContext.GetAnimatorStateComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static CountersHolderComponent GetCountersHolderComponent(this IEntity entity)		{			return entity.ComponentContext.GetCountersHolderComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static TestSerializationComponent GetTestSerializationComponent(this IEntity entity)		{			return entity.ComponentContext.GetTestSerializationComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static TransformComponent GetTransformComponent(this IEntity entity)		{			return entity.ComponentContext.GetTransformComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static AIStrategyComponent GetAIStrategyComponent(this IEntity entity)		{			return entity.ComponentContext.GetAIStrategyComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static UntilSuccessStrategyNodeComponent GetUntilSuccessStrategyNodeComponent(this IEntity entity)		{			return entity.ComponentContext.GetUntilSuccessStrategyNodeComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static AbilitiesHolderComponent GetAbilitiesHolderComponent(this IEntity entity)		{			return entity.ComponentContext.GetAbilitiesHolderComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static AfterLifeCompleteTagComponent GetAfterLifeCompleteTagComponent(this IEntity entity)		{			return entity.ComponentContext.GetAfterLifeCompleteTagComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static AfterLifeTagComponent GetAfterLifeTagComponent(this IEntity entity)		{			return entity.ComponentContext.GetAfterLifeTagComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static GameLogicTagComponent GetGameLogicTagComponent(this IEntity entity)		{			return entity.ComponentContext.GetGameLogicTagComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static InputActionsComponent GetInputActionsComponent(this IEntity entity)		{			return entity.ComponentContext.GetInputActionsComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static InputListenerTagComponent GetInputListenerTagComponent(this IEntity entity)		{			return entity.ComponentContext.GetInputListenerTagComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static ItemTagComponent GetItemTagComponent(this IEntity entity)		{			return entity.ComponentContext.GetItemTagComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static NavMeshAgentComponent GetNavMeshAgentComponent(this IEntity entity)		{			return entity.ComponentContext.GetNavMeshAgentComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static NetworkEntityTagComponent GetNetworkEntityTagComponent(this IEntity entity)		{			return entity.ComponentContext.GetNetworkEntityTagComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static UIGroupTagComponent GetUIGroupTagComponent(this IEntity entity)		{			return entity.ComponentContext.GetUIGroupTagComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static UIViewReferenceComponent GetUIViewReferenceComponent(this IEntity entity)		{			return entity.ComponentContext.GetUIViewReferenceComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static UnityRectTransformComponent GetUnityRectTransformComponent(this IEntity entity)		{			return entity.ComponentContext.GetUnityRectTransformComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static UnityTransformComponent GetUnityTransformComponent(this IEntity entity)		{			return entity.ComponentContext.GetUnityTransformComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static ViewDestructionDelayedComponent GetViewDestructionDelayedComponent(this IEntity entity)		{			return entity.ComponentContext.GetViewDestructionDelayedComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static ViewReferenceGameObjectComponent GetViewReferenceGameObjectComponent(this IEntity entity)		{			return entity.ComponentContext.GetViewReferenceGameObjectComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static SoundVolumeComponent GetSoundVolumeComponent(this IEntity entity)		{			return entity.ComponentContext.GetSoundVolumeComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static AbilityTagComponent GetAbilityTagComponent(this IEntity entity)		{			return entity.ComponentContext.GetAbilityTagComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static PassiveAbilityTag GetPassiveAbilityTag(this IEntity entity)		{			return entity.ComponentContext.GetPassiveAbilityTag;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static ScenarioAnimationComponent GetScenarioAnimationComponent(this IEntity entity)		{			return entity.ComponentContext.GetScenarioAnimationComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static StateContextComponent GetStateContextComponent(this IEntity entity)		{			return entity.ComponentContext.GetStateContextComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static StateDataComponent GetStateDataComponent(this IEntity entity)		{			return entity.ComponentContext.GetStateDataComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static StateInfoComponent GetStateInfoComponent(this IEntity entity)		{			return entity.ComponentContext.GetStateInfoComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static AnimationCheckOutsHolderComponent GetAnimationCheckOutsHolderComponent(this IEntity entity)		{			return entity.ComponentContext.GetAnimationCheckOutsHolderComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static OverrideAnimatorComponent GetOverrideAnimatorComponent(this IEntity entity)		{			return entity.ComponentContext.GetOverrideAnimatorComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static SetupAfterViewTagComponent GetSetupAfterViewTagComponent(this IEntity entity)		{			return entity.ComponentContext.GetSetupAfterViewTagComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static AdditionalCanvasTagComponent GetAdditionalCanvasTagComponent(this IEntity entity)		{			return entity.ComponentContext.GetAdditionalCanvasTagComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static MainCanvasTagComponent GetMainCanvasTagComponent(this IEntity entity)		{			return entity.ComponentContext.GetMainCanvasTagComponent;		}		[MethodImpl(MethodImplOptions.AggressiveInlining)]		public static UITagComponent GetUITagComponent(this IEntity entity)		{			return entity.ComponentContext.GetUITagComponent;		}	}}